using AutoMapper;
using PokemonReviewApi.Dto;
using PokemonReviewApi.Models;

namespace PokemonReviewApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Pokemon,PokemonDto>();
            CreateMap<Category,CategoryDto>();
        }
    }
}
