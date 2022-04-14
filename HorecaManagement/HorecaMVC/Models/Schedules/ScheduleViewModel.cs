using System.ComponentModel.DataAnnotations;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.MVC.Models.Schedules
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        [Required]
        public DateTime ScheduleDate { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be higher than 0.")]
        public int Capacity { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be higher than 0.")]
        public int AvailableSeat { get; set; }
        [Required]
        public ScheduleStatus Status { get; set; }
    }
}
