namespace api_sylvainbreton.Services.Interfaces
{
    using api_sylvainbreton.Models.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArtworkService
    {
        Task<IServiceResult<IEnumerable<ArtworkDTO>>> GetAllArtworksAsync();
        Task<IServiceResult<ArtworkDTO>> GetArtworkByIdAsync(int id);
        Task<IServiceResult<ArtworkDTO>> CreateArtworkAsync(ArtworkDTO artworkDTO);
        Task<IServiceResult<ArtworkDTO>> UpdateArtworkAsync(int id, ArtworkDTO artworkDTO);
        Task<IServiceResult<ArtworkDTO>> DeleteArtworkAsync(int id);

    }
}
