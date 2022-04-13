using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IBookingDetailRepository : IRepository<BookingDetail>
    {
        public Task<BookingDetail> GetDetailsByBookingId(int bookingId);

        public Task<IEnumerable<BookingDetail>> GetDetailsForRestaurantSchedule(int scheduleId);

        public Task CreateBookingDetail(BookingDetail bookingDetail);
    }
}