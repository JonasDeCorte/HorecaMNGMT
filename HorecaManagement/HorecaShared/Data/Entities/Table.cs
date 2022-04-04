namespace Horeca.Shared.Data.Entities
{
    public class Table : BaseEntity
    {
        public int RestaurantScheduleId { get; set; }

        public RestaurantSchedule RestaurantSchedule { get; set; }

        public int? BookingDetailId { get; set; }

        public BookingDetail? BookingDetail { get; set; }

        public Order? Order { get; set; }

        public int? OrderId { get; set; }

        public int Pax { get; set; }
    }
}