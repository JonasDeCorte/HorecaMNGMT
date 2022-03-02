using AutoMapper;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Units;

namespace Horeca.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<Unit, UnitDto>();
            CreateMap<Dish, DishDto>();
        }
    }
}