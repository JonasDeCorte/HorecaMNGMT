namespace Horeca.MVC.Models.Schedules
{
    public class RestaurantScheduleViewModel
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }

        public DateTime ScheduleDate { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int Capacity { get; set; }

        public int AvailableSeat { get; set; }
        public int Status { get; set; }
    }
}
