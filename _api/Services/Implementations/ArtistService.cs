namespace api_sylvainbreton.Services.Implementations
{
    using api_sylvainbreton.Exceptions;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Services.Utilities;
    using api_sylvainbreton.Services.Interfaces;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;

    public class ArtistService(IArtistRepository artistRepository, ILogger<ArtistService> logger, IMapper mapper) : IArtistService
    {
        private readonly IArtistRepository _artistRepository = artistRepository;
        private readonly ILogger<ArtistService> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<IServiceResult<IEnumerable<ArtistDTO>>> GetAllArtistsAsync(int page, int pageSize)
        {
            try
            {
                var artists = await _artistRepository.GetAllArtistsAsync(page, pageSize);
                var artistDTOs = _mapper.Map<IEnumerable<ArtistDTO>>(artists);

                return new ServiceResult<IEnumerable<ArtistDTO>>(true, artistDTOs, "Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all artists.");
                throw new InternalServerErrorException("An error occurred while fetching artists.");
            }
        }

        public async Task<IServiceResult<ArtistDTO>> GetArtistByIdAsync(int id)
        {
            try
            {
                var artist = await _artistRepository.GetArtistByIdAsync(id);
                if (artist == null)
                {
                    return new ServiceResult<ArtistDTO>(false, null, "Artist not found.");
                }

                var artistDTO = _mapper.Map<ArtistDTO>(artist);
                return new ServiceResult<ArtistDTO>(true, artistDTO, "Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching artist by ID: {id}.", id);
                throw new InternalServerErrorException("An error occurred while fetching the artist.");
            }
        }

        public async Task<IServiceResult<ArtistDTO>> CreateArtistAsync(ArtistDTO artistDTO)
        {
            try
            {
                var artist = _mapper.Map<Artist>(artistDTO);
                var createdArtist = await _artistRepository.CreateArtistAsync(artist);
                var resultDTO = _mapper.Map<ArtistDTO>(createdArtist);

                return new ServiceResult<ArtistDTO>(true, resultDTO, "Artist created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating artist.");
                throw new InternalServerErrorException("An error occurred while creating the artist.");
            }
        }

        public async Task<IServiceResult<ArtistDTO>> UpdateArtistAsync(int id, ArtistDTO artistDTO)
        {
            try
            {
                var artistToUpdate = await _artistRepository.GetArtistByIdAsync(id);
                if (artistToUpdate == null)
                {
                    return new ServiceResult<ArtistDTO>(false, null, "Artist not found.");
                }

                _mapper.Map(artistDTO, artistToUpdate);
                await _artistRepository.UpdateArtistAsync(id, artistToUpdate);

                var resultDTO = _mapper.Map<ArtistDTO>(artistToUpdate);
                return new ServiceResult<ArtistDTO>(true, resultDTO, "Artist updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating artist with ID: {id}.", id);
                throw new InternalServerErrorException("An error occurred while updating the artist.");
            }
        }

        public async Task<IServiceResult<ArtistDTO>> DeleteArtistAsync(int id)
        {
            try
            {
                var success = await _artistRepository.DeleteArtistAsync(id);
                if (!success)
                {
                    return new ServiceResult<ArtistDTO>(false, null, "Artist not found.");
                }

                return new ServiceResult<ArtistDTO>(true, null, "Artist deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting artist with ID: {id}.", id);
                throw new InternalServerErrorException("An error occurred while deleting the artist.");
            }
        }
    }
}
