using Horeca.Shared.Dtos.RestaurantSchedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Shared.Dtos.Bookings
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public string BookingNo { get; set; }

        public string BookingStatus { get; set; }

        public string FullName { get; set; }

        public string PhoneNo { get; set; }
    }

    public class BookingDtoInfo
    {
        public string UserID { get; set; }
        public string FullName { get; set; }

        public string PhoneNo { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
    }

    public class BookingDetailDto
    {
        public int BookingId { get; set; }

        public BookingDto Booking { get; set; }

        public int ScheduleId { get; set; }

        public RestaurantScheduleDto RestaurantSchedule { get; set; }

        public int Pax { get; set; }
    }

    public class BookingDetailOnlyBookingsDto
    {
        public int BookingId { get; set; }

        public BookingDto Booking { get; set; }

        public int Pax { get; set; }
    }

    public class BookingHistoryDto
    {
        public List<BookingDetailOnlyBookingsDto> BookingDetails { get; set; } = new();
    }

    public class MakeBookingDto
    {
        public BookingDtoInfo Booking { get; set; }
        public int ScheduleId { get; set; }
        public int Pax { get; set; }
    }

    public class EditBookingDto
    {
        public BookingDtoInfo Booking { get; set; }
        public int BookingId { get; set; }

        public int Pax { get; set; }
    }
}