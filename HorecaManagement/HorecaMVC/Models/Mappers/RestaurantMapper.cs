using Horeca.MVC.Models.Restaurants;
using Horeca.Shared.Dtos.Restaurants;

namespace Horeca.MVC.Models.Mappers
{
    public class RestaurantMapper
    {
        public static RestaurantViewModel MapRestaurantModel(RestaurantDto restaurantDto)
        {
            RestaurantViewModel restaurantModel = new RestaurantViewModel
            {
                Id = restaurantDto.Id,
                Name = restaurantDto.Name,
            };
            return restaurantModel;
        }

        public static RestaurantListViewModel MapRestaurantListModel(IEnumerable<RestaurantDto> restaurantDtos)
        {
            RestaurantListViewModel model = new RestaurantListViewModel();
            foreach (var restaurantDto in restaurantDtos)
            {
                RestaurantViewModel restaurantModel = MapRestaurantModel(restaurantDto);
                model.Restaurants.Add(restaurantModel);
            }
            return model;
        }

        public static RestaurantDetailViewModel MapRestaurantDetailModel(DetailRestaurantDto restaurantDto)
        {
            RestaurantDetailViewModel restaurantDetailModel = new RestaurantDetailViewModel
            {
                Id = restaurantDto.Id,
                Name = restaurantDto.Name,
            };
            restaurantDetailModel.RestaurantSchedules = ScheduleMapper.MapRestaurantScheduleList(restaurantDto.RestaurantSchedules);
            restaurantDetailModel.Employees = AccountMapper.MapUserModelList(restaurantDto.Employees);
            restaurantDetailModel.MenuCards = MenuCardMapper.MapMenuCardModelList(restaurantDto.MenuCards);
            return restaurantDetailModel;
        }

        public static MutateRestaurantDto MapRestaurantDto(CreateRestaurantViewModel model)
        {
            MutateRestaurantDto dto = new MutateRestaurantDto
            {
                Id = model.Id,
                Name = model.Name,
                OwnerName = model.OwnerName,
            };
            return dto;
        }
    }
}
