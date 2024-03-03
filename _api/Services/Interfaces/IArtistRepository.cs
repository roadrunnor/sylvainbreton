using api_sylvainbreton.Models;

namespace api_sylvainbreton.Services.Interfaces
{
    public interface IArtistRepository
    {
        Task<IEnumerable<Artist>> GetAllArtistsAsync(int page, int pageSize);
        Task<Artist> GetArtistByIdAsync(int artistId);
        Task<Artist> CreateArtistAsync(Artist artist);
        Task<Artist> UpdateArtistAsync(int artistId, Artist artist);
        Task<bool> DeleteArtistAsync(int artistId);
    }
}
