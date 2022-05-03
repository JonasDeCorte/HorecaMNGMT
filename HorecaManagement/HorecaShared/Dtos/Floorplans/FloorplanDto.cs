using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Restaurants;

namespace Horeca.Shared.Dtos.Floorplans
{
    public class FloorplanDto
    {
        public int Id { get; set; }
        public RestaurantDto? Restaurant { get; set; }
    }

    public class FloorplanDetailDto : FloorplanDto
    {
        public List<Table> Tables { get; set; }
    }

    public class MutateFloorplanDto : FloorplanDto
    {

    }
}
