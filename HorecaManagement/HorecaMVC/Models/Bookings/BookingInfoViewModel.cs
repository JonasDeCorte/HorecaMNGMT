namespace Horeca.MVC.Models.Bookings
{
    public class BookingInfoViewModel
    {
        public string UserID { get; set; }
        public string FullName { get; set; }

        public string PhoneNo { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
    }
}
