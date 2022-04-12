using static Horeca.Shared.Utils.Constants;

namespace Horeca.Shared.Dtos.RestaurantSchedules
{
    public class RestaurantScheduleDto
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }

        public DateTime ScheduleDate { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int Capacity { get; set; }

        public int AvailableSeat { get; set; }
        public ScheduleStatus Status { get; set; }
    }

    public class RestaurantScheduleByIdDto : RestaurantScheduleDto
    {
        public string RestaurantName { get; set; }
    }

    public class MutateRestaurantScheduleDto : RestaurantScheduleDto
    {
    }
}