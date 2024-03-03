namespace api_sylvainbreton.Profiles
{
    using AutoMapper;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Models.DTOs;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Artist, ArtistDTO>().ReverseMap();
            CreateMap<Artwork, ArtworkDTO>();
            CreateMap<ArtworkImage, ArtworkImageDTO>();
            CreateMap<Image, ImageDTO>();
            CreateMap<Sentence, SentenceDTO>();
        }
    }

}
