using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.MenuCards;

namespace Horeca.Shared.Dtos.Restaurants
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DetailRestaurantDto : RestaurantDto
    {
        public List<BaseUserDto> Employees { get; set; }
        public List<MenuCardDto> MenuCards { get; set; }
    }
}