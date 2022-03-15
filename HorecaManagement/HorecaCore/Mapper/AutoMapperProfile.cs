﻿using AutoMapper;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Ingredients;
using Horeca.Shared.Dtos.MenuCards;
using Horeca.Shared.Dtos.Menus;
using Horeca.Shared.Dtos.Reservation;
using Horeca.Shared.Dtos.Tables;
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
            CreateMap<Dish, DishIngredientsByIdDto>();
            CreateMap<Menu, MenuDishesByIdDto>();
            CreateMap<Menu, MenuDto>();
            CreateMap<MenuCard, MenuCardDto>();
            CreateMap<MenuCard, MenuCardsByIdDto>();
            CreateMap<Table, TableDto>();
            CreateMap<Reservation, ReservationDto>();
        }
    }
}