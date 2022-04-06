namespace Horeca.MVC.Models.Bookings
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public DateTime BookingDate { get; set; }

        public string BookingNo { get; set; }

        public string BookingStatus { get; set; }

        public string FullName { get; set; }
    }
}
