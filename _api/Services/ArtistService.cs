﻿namespace api_sylvainbreton.Services
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ArtistService(SylvainBretonDbContext context, ISanitizationService sanitizationService, ILogger<ArtistService> logger) : IArtistService
    {
        private readonly SylvainBretonDbContext _context = context;
        private readonly ISanitizationService _sanitizationService = sanitizationService;
        private readonly ILogger<ArtistService> _logger = logger;

        public async Task<IServiceResult<IEnumerable<ArtistDTO>>> GetAllArtistsAsync(int page, int pageSize)
        {
            try
            {
                var totalRecords = await _context.Artists.CountAsync();
                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                var artists = await _context.Artists
                    .AsNoTracking()
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(a => new ArtistDTO
                    {
                        ArtistID = a.ArtistID,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    })
                    .ToListAsync();

                var pagination = new PaginationDetails 
                { 
                    TotalRecords = totalRecords, 
                    PageSize = pageSize, 
                    CurrentPage = page, 
                    TotalPages = totalPages 
                };

                return new ServiceResult<IEnumerable<ArtistDTO>>(artists, pagination);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in GetAllArtistsAsync: {Message}", ex.Message);
                return new ServiceResult<IEnumerable<ArtistDTO>>(false, null, "An error occurred while retrieving artists.", 500);
            }
        }

        public async Task<IServiceResult<ArtistDTO>> GetArtistByIdAsync(int id)
        {
            try
            {
                var artist = await _context.Artists
                    .AsNoTracking()
                    .Select(a => new ArtistDTO
                    {
                        ArtistID = a.ArtistID,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    })
                    .FirstOrDefaultAsync(a => a.ArtistID == id);

                if (artist == null)
                {
                    _logger.LogWarning("Artist with ID {Id} not found.", id);
                    return new ServiceResult<ArtistDTO>(false, null, "Artist not found.", 404);
                }

                return new ServiceResult<ArtistDTO>(artist);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in GetArtistByIdAsync: {Message}", ex.Message);
                return new ServiceResult<ArtistDTO>(false, null, "An error occurred while retrieving the artist.", 500);
            }
        }

        public async Task<IServiceResult<ArtistDTO>> CreateArtistAsync(ArtistDTO artistDTO)
        {
            try
            {
                var artist = new Artist
                {
                    FirstName = _sanitizationService.SanitizeInput(artistDTO.FirstName),
                    LastName = _sanitizationService.SanitizeInput(artistDTO.LastName),
                    Bio = _sanitizationService.SanitizeInput(artistDTO.Bio)
                };

                _context.Artists.Add(artist);
                await _context.SaveChangesAsync();

                artistDTO.ArtistID = artist.ArtistID; // Update DTO with new ID

                _logger.LogInformation("Artist with ID {Id} created successfully.", artistDTO.ArtistID);
                return new ServiceResult<ArtistDTO>(artistDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in CreateArtistAsync: {Message}", ex.Message);
                return new ServiceResult<ArtistDTO>(false, null, "An error occurred while creating the artist.", 500);
            }
        }

        public async Task<IServiceResult<ArtistDTO>> UpdateArtistAsync(int id, ArtistDTO artistDTO)
        {
            try
            {
                if (id != artistDTO.ArtistID)
                {
                    return new ServiceResult<ArtistDTO>(false, null, "Artist ID mismatch.", 400);
                }

                var artist = await _context.Artists.FindAsync(id);
                if (artist == null)
                {
                    _logger.LogWarning("Artist with ID {Id} not found.", id);
                    return new ServiceResult<ArtistDTO>(false, null, "Artist not found.", 404);
                }

                artist.FirstName = _sanitizationService.SanitizeInput(artistDTO.FirstName);
                artist.LastName = _sanitizationService.SanitizeInput(artistDTO.LastName);
                artist.Bio = _sanitizationService.SanitizeInput(artistDTO.Bio);

                _context.Update(artist);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Artist with ID {Id} updated successfully.", id);
                return new ServiceResult<ArtistDTO>(artistDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in UpdateArtistAsync: {Message}", ex.Message);
                return new ServiceResult<ArtistDTO>(false, null, "An error occurred while updating the artist.", 500);
            }
        }

        public async Task<IServiceResult<ArtistDTO>> DeleteArtistAsync(int id)
        {
            try
            {
                var artist = await _context.Artists.FindAsync(id);
                if (artist == null)
                {
                    _logger.LogWarning("Artist with ID {Id} not found.", id);
                    return new ServiceResult<ArtistDTO>(false, null, "Artist not found.", 404);
                }

                _context.Artists.Remove(artist);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Artist with ID {Id} deleted successfully.", id);
                return new ServiceResult<ArtistDTO>(true, null, "Artist deleted successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in DeleteArtistAsync: {Message}", ex.Message);
                return new ServiceResult<ArtistDTO>(false, null, "An error occurred while deleting the artist.", 500);
            }
        }
    }
}