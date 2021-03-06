using Horeca.MVC.Models.Tables;
using Horeca.Shared.Dtos.Orders;
using Horeca.Shared.Dtos.Tables;

namespace Horeca.MVC.Helpers.Mappers
{
    public class TableMapper
    {
        public static TableViewModel MapTableModel(TableDto response)
        {
            return new TableViewModel()
            {
                Id = response.Id,
                FloorplanId = response.FloorplanId,
                ScheduleId = response.ScheduleId,
                BookingDetailId = response.BookingDetailId,
                Seats = response.Seats,
                Name = response.Name,
                Src = response.Src,
            };
        }

        public static EditTableViewModel MapEditTableModel(TableDto response)
        {
            return new EditTableViewModel()
            {
                Id = response.Id,
                FloorplanId = response.FloorplanId,
                Seats = response.Seats,
                Name = response.Name,
            };
        }

        public static TableDetailViewModel MapTableDetailModel(TableDto response, List<GetOrderLinesByTableIdDto> orders, int existingDishes)
        {
            TableDetailViewModel model = new()
            {
                Id = response.Id,
                FloorplanId = response.FloorplanId,
                ScheduleId = response.ScheduleId,
                BookingDetailId = response.BookingDetailId,
                Seats = response.Seats,
                Name = response.Name,
                Src = response.Src,
                ExistingDishes = existingDishes,
            };
            foreach (var order in orders)
            {
                model.Orders.Add(OrderMapper.MapOrderModel(order));
            }
            return model;
        }

        public static FloorplanTableViewModel MapFloorplanTableModel(MutateTableDto table)
        {
            return new FloorplanTableViewModel
            {
                Id = table.Id,
                FloorplanId = table.FloorplanId,
                ScheduleId = table.ScheduleId,
                BookingDetailId = table.BookingDetailId,
                Seats = table.Seats,
                Name = table.Name,
                Src = table.Src,
                Type = table.Type,
                OriginX = table.OriginX,
                OriginY = table.OriginY,
                Left = table.Left,
                Top = table.Top,
                Width = table.Width,
                Height = table.Height,
                ScaleX = table.ScaleX,
                ScaleY = table.ScaleY,
            };
        }

        public static EditTableDto MapEditTableDto(EditTableViewModel table, int floorplanId)
        {
            return new EditTableDto
            {
                FloorplanId = floorplanId,
                Id = table.Id,
                Name = table.Name,
                Seats = table.Seats,
                Pax = Convert.ToInt32(table.Seats),
            };
        }

        public static GetCanvasTableViewModel MapCanvasTableModel(MutateTableDto table)
        {
            return new GetCanvasTableViewModel()
            {
                id = table.Id,
                name = table.Name,
                pax = table.Pax,
                seats = table.Seats,
                src = table.Src,
                type = table.Type,
                originX = table.OriginX,
                originY = table.OriginY,
                left = table.Left,
                top = table.Top,
                width = table.Width,
                height = table.Height,
                scaleX = (int)table.ScaleX,
                scaleY = (int)table.ScaleY,
            };
        }

        public static MutateTableDto MapMutateTableDto(CanvasTableViewModel table, int floorplanId, int restaurantId)
        {
            return new MutateTableDto()
            {
                FloorplanId = floorplanId,
                ScheduleId = restaurantId,
                Pax = Convert.ToInt32(table.Seats),
                Seats = table.Seats,
                Name = table.Name,
                Src = table.src,
                Type = table.type,
                OriginX = table.originX,
                OriginY = table.originY,
                Left = (int)table.left,
                Top = (int)table.top,
                Width = table.width,
                Height = table.height,
                ScaleX = table.scaleX,
                ScaleY = table.scaleY,
            };
        }
    }
}