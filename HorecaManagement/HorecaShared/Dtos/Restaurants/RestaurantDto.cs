using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.MenuCards;
using Horeca.Shared.Dtos.Schedules;

namespace Horeca.Shared.Dtos.Restaurants
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DetailRestaurantDto : RestaurantDto
    {
        public List<ScheduleDto> Schedules { get; set; } = new();
        public List<BaseUserDto> Employees { get; set; } = new();
        public List<MenuCardDto> MenuCards { get; set; } = new();
    }

    public class MutateRestaurantDto : RestaurantDto
    {
        public string OwnerName { get; set; }
    }

    public class EditRestaurantDto : RestaurantDto
    {
    }
}