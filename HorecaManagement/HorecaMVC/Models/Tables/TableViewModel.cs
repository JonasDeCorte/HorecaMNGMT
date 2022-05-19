using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Tables
{
    public class TableViewModel
    {
        public int Id { get; set; }

        public int FloorplanId { get; set; }

        public int? ScheduleId { get; set; }

        public int BookingDetailId { get; set; }

        [Required]
        public string? Seats { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string Name { get; set; }

        public string Src { get; set; }
    }
}
