using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        public int AdminGetPendingBookingCount();

        public Task<Booking> GetByNumber(string bookingNo);

        public Task<Booking> GetBookingByID(int bookingID);

        public Task<IEnumerable<Booking>> GetByUserID(string userID, string status);

        public new Task<Booking> Add(Booking booking);
    }
}