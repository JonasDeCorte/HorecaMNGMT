using Horeca.MVC.Models.Tables;
using Horeca.Shared.Dtos.Tables;

namespace Horeca.MVC.Helpers.Mappers
{
    public class TableMapper
    {
        public static TableDetailViewModel MapTableDetailModel(MutateTableDto table)
        {
            return new TableDetailViewModel()
            {
                Id = table.Id,
                FloorplanId = table.FloorplanId,
                ScheduleId = table.ScheduleId,
                BookingDetailId = table.BookingDetailId,
                Pax = table.Pax,
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

        public static MutateTableDto MapMutateTableDto(CanvasTableViewModel table, int floorplanId)
        {
            return new MutateTableDto()
            {
                FloorplanId = floorplanId,
                ScheduleId = floorplanId,
                BookingDetailId = floorplanId,
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
