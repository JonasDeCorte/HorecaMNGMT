namespace Horeca.Shared.Dtos.Bookings
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public string BookingNo { get; set; }

        public string BookingStatus { get; set; }

        public string FullName { get; set; }

        public string PhoneNo { get; set; }
        public int ScheduleId { get; set; }
        public int? RestaurantId { get; set; }

        public int Pax { get; set; }
    }

    public class BookingHistoryDto
    {
        public List<BookingDto> BookingDetails { get; set; } = new();
    }

    public class MakeBookingDto
    {
        public string UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public string FullName { get; set; }

        public string PhoneNo { get; set; }
        public int ScheduleId { get; set; }
        public int RestaurantId { get; set; }

        public int Pax { get; set; }
    }

    public class EditBookingDto
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public string FullName { get; set; }

        public string PhoneNo { get; set; }
        public int ScheduleId { get; set; }

        public int Pax { get; set; }
    }
}