using Horeca.MVC.Models.Restaurants;
using Horeca.Shared.Dtos.Restaurants;

namespace Horeca.MVC.Models.Mappers
{
    public class RestaurantMapper
    {
        public static RestaurantViewModel MapRestaurantModel(RestaurantDto restaurantDto)
        {
            return new RestaurantViewModel()
            {
                Id = restaurantDto.Id,
                Name = restaurantDto.Name,
            };
        }

        public static RestaurantListViewModel MapRestaurantListModel(IEnumerable<RestaurantDto> restaurantDtos)
        {
            RestaurantListViewModel model = new();
            foreach (var restaurantDto in restaurantDtos)
            {
                RestaurantViewModel restaurantModel = MapRestaurantModel(restaurantDto);
                model.Restaurants.Add(restaurantModel);
            }
            return model;
        }

        public static RestaurantDetailViewModel MapRestaurantDetailModel(DetailRestaurantDto restaurantDto)
        {
            RestaurantDetailViewModel restaurantDetailModel = new()
            {
                Id = restaurantDto.Id,
                Name = restaurantDto.Name,
            };
            restaurantDetailModel.RestaurantScheduleListViewModel = ScheduleMapper.MapRestaurantScheduleList(restaurantDto.RestaurantSchedules);
            restaurantDetailModel.Employees = AccountMapper.MapUserModelList(restaurantDto.Employees);
            restaurantDetailModel.MenuCards = MenuCardMapper.MapMenuCardModelList(restaurantDto.MenuCards);
            return restaurantDetailModel;
        }

        public static MutateRestaurantViewModel MapMutateRestaurantModel(DetailRestaurantDto dto)
        {
            return new MutateRestaurantViewModel()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static MutateRestaurantDto MapCreateRestaurantDto(MutateRestaurantViewModel model)
        {
            return new MutateRestaurantDto()
            {
                Id = model.Id,
                Name = model.Name,
                OwnerName = model.OwnerName,
            };
        }

        public static EditRestaurantDto MapEditRestaurantDto(RestaurantViewModel model)
        {
            return new EditRestaurantDto()
            {
                Id = model.Id,
                Name = model.Name,
            };
        }
    }
}