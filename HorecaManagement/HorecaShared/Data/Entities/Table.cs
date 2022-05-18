namespace Horeca.Shared.Data.Entities
{
    public class Table : BaseEntity
    {
        public int FloorplanId { get; set; }

        public int? ScheduleId { get; set; }

        public Schedule? Schedule { get; set; }

        public int? BookingId { get; set; }

        public Booking? Booking { get; set; }

        public List<Order> Orders { get; set; } = new();

        public string Name { get; set; }

        public string? Seats { get; set; }

        public int? Pax { get; set; }

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