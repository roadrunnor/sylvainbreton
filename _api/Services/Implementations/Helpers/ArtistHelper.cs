using api_sylvainbreton.Exceptions;
using api_sylvainbreton.Models.DTOs;

namespace api_sylvainbreton.Services.Implementations.Helpers
{
    public static class ArtistHelper
    {
        public static void ValidateArtistDTO(ArtistDTO artistDTO)
        {
            if (artistDTO.FirstName.Length > 100)
            {
                throw new BadRequestException("Firstname cannot be longer than 100 characters.");
            }

            if (artistDTO.LastName.Length > 100)
            {
                throw new BadRequestException("Lastname cannot be longer than 100 characters.");
            }

            if (artistDTO.Bio.Length < 10)
            {
                throw new BadRequestException("Bio must be at least 10 characters long.");
            }
        }
    }
}
