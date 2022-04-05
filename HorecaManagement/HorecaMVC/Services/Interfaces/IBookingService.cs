using Horeca.Shared.Dtos.Bookings;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IBookingService
    {
        public Task<int> GetPendingBookings();
        public Task<BookingDto> GetBookingByNumber(int bookingNo);
        public Task<IEnumerable<BookingDto>> GetBookingsByStatus(string status);
        public Task<IEnumerable<BookingHistoryDto>> GetBookingsByUserId(string userId, string status);
        public Task<HttpResponseMessage> AddBooking(MakeBookingDto bookingDto);
        public Task<HttpResponseMessage> UpdateBooking(EditBookingDto bookingDto);
        public Task<HttpResponseMessage> DeleteBooking(int id);
    }
}
