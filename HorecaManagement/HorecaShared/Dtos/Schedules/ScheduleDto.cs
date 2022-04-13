using static Horeca.Shared.Utils.Constants;

namespace Horeca.Shared.Dtos.Schedules
{
    public class ScheduleDto
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

    public class ScheduleByIdDto : ScheduleDto
    {
        public string RestaurantName { get; set; }
    }

    public class MutateScheduleDto : ScheduleDto
    {
    }
}