using AutoMapper;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.Bookings;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Ingredients;
using Horeca.Shared.Dtos.MenuCards;
using Horeca.Shared.Dtos.Menus;
using Horeca.Shared.Dtos.Orders;
using Horeca.Shared.Dtos.Restaurants;
using Horeca.Shared.Dtos.RestaurantSchedules;
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
            CreateMap<Restaurant, DetailRestaurantDto>();
            CreateMap<Permission, PermissionDto>();
            CreateMap<ApplicationUser, BaseUserDto>();
            CreateMap<Booking, BookingDto>();
            CreateMap<RestaurantSchedule, RestaurantScheduleDto>();
            CreateMap<RestaurantSchedule, RestaurantScheduleByIdDto>();
            CreateMap<Restaurant, DetailRestaurantDto>();
            CreateMap<Table, TableDto>();
            CreateMap<Order, OrderLinesByOrderIdDto>()
                .ForMember(dest => dest.Lines, act => act.MapFrom(src => src.OrderLines));
            CreateMap<Order, OrderDto>();

            CreateMap<OrderLine, OrderLineDto>();
        }
    }
}