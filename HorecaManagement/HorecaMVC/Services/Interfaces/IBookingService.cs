using Horeca.Shared.Dtos.Bookings;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IBookingService
    {
        public Task<int> GetPendingBookings();
        public Task<BookingDto> GetBookingByNumber(string bookingNo);
        public Task<IEnumerable<BookingDto>> GetBookingsByStatus(int scheduleId, string status);
        public Task<BookingHistoryDto> GetBookingsByUserId(string userId, string status);
        public Task<IEnumerable<BookingDto>> GetBookingsBySchedule(int scheduleId);
        public Task<HttpResponseMessage> AddBooking(MakeBookingDto bookingDto);
        public Task<HttpResponseMessage> UpdateBooking(EditBookingDto bookingDto);
        public Task<HttpResponseMessage> DeleteBooking(int id);
    }
}
