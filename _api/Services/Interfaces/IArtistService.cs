namespace api_sylvainbreton.Services.Interfaces
{
    using api_sylvainbreton.Models.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArtistService
    {
        Task<IServiceResult<IEnumerable<ArtistDTO>>> GetAllArtistsAsync(int page, int pageSize);
        Task<IServiceResult<ArtistDTO>> GetArtistByIdAsync(int id);
        Task<IServiceResult<ArtistDTO>> CreateArtistAsync(ArtistDTO artistDTO);
        Task<IServiceResult<ArtistDTO>> UpdateArtistAsync(int id, ArtistDTO artistDTO);
        Task<IServiceResult<ArtistDTO>> DeleteArtistAsync(int id);
    }
}
