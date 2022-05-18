using Horeca.MVC.Helpers.Attributes;
using Horeca.Shared.Constants;
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
        [Date(ErrorMessage = "Date must not be in the past.")]
        public DateTime ScheduleDate { get; set; }

        [Display(Name = "Start Time")]
        [Required]
        [DateSmallerThan("EndTime", ErrorMessage = ErrorConstants.StartTimeEarlier)]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.AboveZero)]
        public int Capacity { get; set; }

        [Display(Name = "Available Seats")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.AboveZero)]
        [SmallerThan("Capacity", ErrorMessage = ErrorConstants.SeatsSmaller)]
        public int AvailableSeat { get; set; }

        [Required]
        public ScheduleStatus Status { get; set; }
    }
}