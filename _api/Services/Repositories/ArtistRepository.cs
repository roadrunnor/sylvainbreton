namespace api_sylvainbreton.Services.Repositories
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Exceptions;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using MySqlConnector;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ArtistRepository(SylvainBretonDbContext context, ISanitizationService sanitizationService, IMemoryCache memoryCache, ILogger<ArtistRepository> logger) : IArtistRepository
    {
        private readonly SylvainBretonDbContext _context = context;
        private readonly ISanitizationService _sanitizationService = sanitizationService;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly ILogger<ArtistRepository> _logger = logger;

        public async Task<IEnumerable<Artist>> GetAllArtistsAsync(int page, int pageSize)
        {
            string cacheKey = $"Artists_Page_{page}_Size_{pageSize}";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Artist> cachedArtists))
            {
                _logger.LogInformation("Cache miss for {cacheKey}. Retrieving artists from database.", cacheKey);
                var artistsQuery = _context.Artists
                    .OrderBy(a => a.ArtistID)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                cachedArtists = await artistsQuery.ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .RegisterPostEvictionCallback((key, value, reason, state) =>
                    {
                        _logger.LogInformation("Cache entry for key {key} was evicted due to {reason}.", key, reason);
                    });
                _memoryCache.Set(cacheKey, cachedArtists, cacheEntryOptions);
            }
            else
            {
                _logger.LogInformation("Cache hit for {cacheKey}.", cacheKey);
            }

            return cachedArtists;
        }

        public async Task<Artist> GetArtistByIdAsync(int artistId)
        {
            string cacheKey = $"Artist_Id_{artistId}";
            if (!_memoryCache.TryGetValue(cacheKey, out Artist cachedArtist))
            {
                _logger.LogInformation("Cache miss for {cacheKey}. Retrieving artist with ID {artistId} from database.", cacheKey, artistId);
                cachedArtist = await _context.Artists
                    .FirstOrDefaultAsync(a => a.ArtistID == artistId);

                if (cachedArtist != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(30))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                        .RegisterPostEvictionCallback((key, value, reason, state) =>
                        {
                            _logger.LogInformation("Cache entry for key {key} was evicted due to {reason}.", key, reason);
                        });
                    _memoryCache.Set(cacheKey, cachedArtist, cacheEntryOptions);
                }
            }
            else
            {
                _logger.LogInformation("Cache hit for {cacheKey}.", cacheKey);
            }

            return cachedArtist;
        }

        public async Task<Artist> CreateArtistAsync(Artist artist)
        {
            if (artist == null) throw new ArgumentNullException(nameof(artist));

            try
            {
                artist.FirstName = _sanitizationService.SanitizeInput(artist.FirstName);
                artist.LastName = _sanitizationService.SanitizeInput(artist.LastName);
                artist.Bio = _sanitizationService.SanitizeInput(artist.Bio);

                await _context.Artists.AddAsync(artist);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Artist {ArtistName} created successfully.", $"{artist.FirstName} {artist.LastName}");
                return artist;
            }
            catch (DbUpdateException ex) when ((ex.InnerException as MySqlException)?.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                // Log the error for duplicate entry and throw a BadRequestException indicating a duplicate artist entry.
                _logger.LogError(ex, "Duplicate entry for {ArtistName}.", $"{artist.FirstName} {artist.LastName}");
                throw new api_sylvainbreton.Exceptions.BadRequestException($"An artist with the name {artist.FirstName} {artist.LastName} already exists.");
            }
            catch (Exception ex)
            {
                // Log the generic error and throw an InternalServerErrorException to indicate an unexpected error during artist creation.
                _logger.LogError(ex, "An error occurred while creating the artist {ArtistName}.", $"{artist.FirstName} {artist.LastName}");
                throw new api_sylvainbreton.Exceptions.InternalServerErrorException("An unexpected error occurred while creating the artist.");
            }
        }

        public async Task<Artist> UpdateArtistAsync(int artistId, Artist artist)
        {
            if (artist != null)
            {
                var existingArtist = await _context.Artists.FindAsync(artistId);

                if (existingArtist == null)
                {
                    _logger.LogWarning("Artist {ArtistID} not found for update.", artistId);
                    return null;
                }

                existingArtist.FirstName = _sanitizationService.SanitizeInput(artist.FirstName);
                existingArtist.LastName = _sanitizationService.SanitizeInput(artist.LastName);
                existingArtist.Bio = _sanitizationService.SanitizeInput(artist.Bio);

                _context.Artists.Update(existingArtist);
                await _context.SaveChangesAsync();

                // Invalidate cache after update
                string cacheKey = $"Artist_Id_{artistId}";
                _memoryCache.Remove(cacheKey);

                _logger.LogInformation("Artist {artistId} updated successfully.", artistId);
                return existingArtist;
            }

            throw new ArgumentNullException(nameof(artist));
        }

        public async Task<bool> DeleteArtistAsync(int artistId)
        {
            var artist = await _context.Artists.FindAsync(artistId);
            if (artist == null)
            {
                _logger.LogWarning("Artist {ArtistID} not found for update.", artistId);
                return false;
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            // Invalidate cache after deletion
            string cacheKey = $"Artist_Id_{artistId}";
            _memoryCache.Remove(cacheKey);

            _logger.LogInformation("Artist {artistId} deleted successfully.", artistId);
            return true;
        }
    }
}
