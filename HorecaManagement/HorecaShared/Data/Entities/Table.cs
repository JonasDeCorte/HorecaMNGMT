namespace Horeca.Shared.Data.Entities
{
    public class Table : BaseEntity
    {
        public int RestaurantScheduleId { get; set; }

        public RestaurantSchedule RestaurantSchedule { get; set; }

        public int Pax { get; set; }
    }
}