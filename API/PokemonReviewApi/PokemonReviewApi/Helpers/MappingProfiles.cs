using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PokemonReviewApi.Dto;
using PokemonReviewApi.Models;

namespace PokemonReviewApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Pokemon,PokemonDto>();
            CreateMap<PokemonDto,Pokemon>();
            CreateMap<Category,CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Country,CountryDto>();
            CreateMap<Owner, OwnerDto>();
        }
    }
}
