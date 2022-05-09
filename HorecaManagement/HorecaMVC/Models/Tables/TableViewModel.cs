namespace Horeca.MVC.Models.Tables
{
    public class TableViewModel
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
}
