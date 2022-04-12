using static Horeca.Shared.Utils.Constants;

namespace Horeca.Shared.Data.Entities
{
    public class RestaurantSchedule : BaseEntity
    {
        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public DateTime ScheduleDate { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int Capacity { get; set; }

        public int AvailableSeat { get; set; }
        public ScheduleStatus Status { get; set; }
    }
}