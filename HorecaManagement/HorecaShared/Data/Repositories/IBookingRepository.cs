using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        public Task<Booking> GetByNumber(string bookingNo);

        public Task<Booking> GetBookingById(int bookingID);

        public Task<Booking> Add(Booking booking);

        public Task<IEnumerable<Booking>> GetAllBookings(int scheduleId);

        public Task<IEnumerable<Booking>> GetBookingsForRestaurantSchedule(int scheduleId);

        public Task<IEnumerable<Booking>> GetDetailsByUserId(string userId, string status);
    }
}