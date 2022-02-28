using AutoMapper;
using HorecaAPI.Data.Entities;
using HorecaShared.Dtos;

namespace Horeca.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ingredient, IngredientDto>();
        }
    }
}