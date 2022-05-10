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
    }
}
