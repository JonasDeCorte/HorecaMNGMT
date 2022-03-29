using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.MenuCards;
using Horeca.Shared.Dtos.RestaurantSchedules;

namespace Horeca.Shared.Dtos.Restaurants
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DetailRestaurantDto : RestaurantDto
    {
        public List<RestaurantScheduleDto> RestaurantSchedules { get; set; }
        public List<BaseUserDto> Employees { get; set; }
        public List<MenuCardDto> MenuCards { get; set; }
    }

    public class MutateRestaurantDto : RestaurantDto
    {
        public string OwnerName { get; set; }
    }

    public class EditRestaurantDto : RestaurantDto
    {
    }
}