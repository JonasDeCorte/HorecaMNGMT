namespace Horeca.MVC.Models.Bookings
{
    public class BookingDetailViewModel : BookingViewModel
    {
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public string PhoneNo { get; set; }
    }
}
