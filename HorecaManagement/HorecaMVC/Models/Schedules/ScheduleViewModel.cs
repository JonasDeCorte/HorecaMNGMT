using Horeca.MVC.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.MVC.Models.Schedules
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        [Display(Name = "Schedule Date")]
        [Required]
        public DateTime ScheduleDate { get; set; }

        [Display(Name = "Start Time")]
        [Required]
        [DateSmallerThan("EndTime", ErrorMessage = "Start Time must be earlier than End Time.")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be higher than 0.")]
        public int Capacity { get; set; }

        [Display(Name = "Available Seats")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be higher than 0.")]
        [SmallerThan("Capacity", ErrorMessage = "Available seats must be lower than or equal to capacity.")]
        public int AvailableSeat { get; set; }

        [Required]
        public ScheduleStatus Status { get; set; }
    }
}
