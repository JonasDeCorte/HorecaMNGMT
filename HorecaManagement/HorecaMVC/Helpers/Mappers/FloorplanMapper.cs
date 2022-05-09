using Horeca.MVC.Models.Floorplans;
using Horeca.Shared.Dtos.Floorplans;

namespace Horeca.MVC.Helpers.Mappers
{
    public class FloorplanMapper
    {
        public static FloorplanListViewModel MapFloorplanListModel(IEnumerable<FloorplanDto> floorplanDtos)
        {
            FloorplanListViewModel model = new FloorplanListViewModel();
            foreach (var floorplanDto in floorplanDtos)
            {
                model.Floorplans.Add(MapFloorplanModel(floorplanDto));
            }
            return model;
        }

        public static FloorplanViewModel MapFloorplanModel(FloorplanDto floorplanDto)
        {
            return new FloorplanViewModel()
            {
                Id = floorplanDto.Id,
                RestaurantId = floorplanDto.Restaurant.Id,
                Name = floorplanDto.Name,
            };
        }

        public static FloorplanDetailViewModel MapFloorplanDetailModel(FloorplanDetailDto floorplanDto)
        {
            FloorplanDetailViewModel model = new FloorplanDetailViewModel()
            {
                Id = floorplanDto.Id,
                RestaurantId = floorplanDto.Restaurant.Id,
                Name = floorplanDto.Name
            };
            foreach(var table in floorplanDto.Tables)
            {
                model.Tables.Add(TableMapper.MapTableDetailModel(table));
            }
            return model;
        }
    }
}
