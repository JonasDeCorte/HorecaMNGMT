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
    }
}
