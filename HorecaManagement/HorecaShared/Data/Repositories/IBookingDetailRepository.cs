using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IBookingDetailRepository : IRepository<BookingDetail>
    {
        public Task<BookingDetail> GetDetailsByID(int bookingID);

        public Task CreateBookingDetail(BookingDetail bookingDetail);
    }
}