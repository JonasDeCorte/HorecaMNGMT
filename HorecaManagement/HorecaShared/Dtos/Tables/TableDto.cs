using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Dtos.Tables
{
    public class TableDto
    {
        public int Id { get; set; }

        public int FloorplanId { get; set; }

        public int? ScheduleId { get; set; }

        public int BookingDetailId { get; set; }

        public int? Pax { get; set; }

        public string? Seats { get; set; }

        public string Name { get; set; }

        public string Src { get; set; }
    }

    public class TableDetailDto : TableDto
    {
        public List<Order> Orders { get; set; }
    }

    public class EditTableDto
    {
        public int Id { get; set; }

        public int FloorplanId { get; set; }

        public int? Pax { get; set; }

        public string? Seats { get; set; }

        public string Name { get; set; }
    }

    public class MutateTableDto
    {
        public int Id { get; set; }

        public int FloorplanId { get; set; }

        public int? ScheduleId { get; set; }

        public int BookingDetailId { get; set; }

        public int? Pax { get; set; }

        public string? Seats { get; set; }

        public string Name { get; set; }

        public string Src { get; set; }

        public string Type { get; set; }

        public string OriginX { get; set; }

        public string OriginY { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public double ScaleX { get; set; }

        public double ScaleY { get; set; }
    }
}