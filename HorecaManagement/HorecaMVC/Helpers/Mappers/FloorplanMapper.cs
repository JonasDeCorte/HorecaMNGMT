using Horeca.MVC.Models.Floorplans;
using Horeca.MVC.Models.Tables;
using Horeca.Shared.Dtos.Floorplans;
using Horeca.Shared.Dtos.Restaurants;
using Horeca.Shared.Dtos.Tables;

namespace Horeca.MVC.Helpers.Mappers
{
    public class FloorplanMapper
    {
        public static FloorplanListViewModel MapFloorplanListModel(IEnumerable<FloorplanDto> floorplanDtos)
        {
            FloorplanListViewModel model = new();
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
            FloorplanDetailViewModel model = new()
            {
                Id = floorplanDto.Id,
                RestaurantId = floorplanDto.Restaurant.Id,
                Name = floorplanDto.Name
            };
            foreach (var table in floorplanDto.Tables)
            {
                model.Tables.Add(TableMapper.MapFloorplanTableModel(table));
            }
            return model;
        }

        public static FloorplanDetailDto MapFloorplanDetailDto(FloorplanCanvasViewModel model, int floorplanId, int restaurantId)
        {
            FloorplanDetailDto dto = new()
            {
                Id = floorplanId,
                Name = "string",
                Restaurant = new RestaurantDto()
                {
                    Id = restaurantId,
                    Name = "string",
                }
            };
            dto.Tables = new List<MutateTableDto>();
            foreach (var table in model.objects)
            {
                var tableDto = TableMapper.MapMutateTableDto(table, floorplanId);
                dto.Tables.Add(tableDto);
            }
            return dto;
        }

        public static GetFloorplanCanvasViewModel MapFloorplanCanvasModel(FloorplanDetailDto floorplan)
        {
            GetFloorplanCanvasViewModel model = new()
            {
                objects = new List<GetCanvasTableViewModel>()
            };
            foreach (var table in floorplan.Tables)
            {
                var canvasTable = TableMapper.MapCanvasTableModel(table);
                model.objects.Add(canvasTable);
            }
            return model;
        }

        public static MutateFloorplanDto MapMutateFloorplanDto(FloorplanViewModel floorplan, RestaurantDto restaurant)
        {
            return new MutateFloorplanDto
            {
                Id = floorplan.Id,
                Name = floorplan.Name,
                Restaurant = restaurant
            };
        }
    }
}