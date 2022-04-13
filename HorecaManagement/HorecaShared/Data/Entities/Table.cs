namespace Horeca.Shared.Data.Entities
{
    public class Table : BaseEntity
    {
        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }

        public int? BookingDetailId { get; set; }

        public BookingDetail? BookingDetail { get; set; }

        public List<Order> Orders { get; set; } = new();

        public int Pax { get; set; }
    }
}