using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories

{
    public class BookingDetailRepository : Repository<BookingDetail>, IBookingDetailRepository
    {
        private readonly DatabaseContext context;

        public BookingDetailRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task CreateBookingDetail(BookingDetail bookingDetail)
        {
            var schedule = context.RestaurantSchedules.Find(bookingDetail.RestaurantScheduleId);

            using var transaction = context.Database.BeginTransaction();
            try
            {
                // Update Schedule AvailableSeat Value
                schedule.AvailableSeat -= bookingDetail.Pax;
                context.RestaurantSchedules.Update(schedule);
                context.BookingDetails.Add(bookingDetail);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        public async Task<BookingDetail> GetDetailsByID(int bookingID)
        {
            return await context.BookingDetails.Include(b => b.RestaurantSchedule)
                                           .ThenInclude(b => b.Restaurant)
                                           .Include(x => x.Booking)
                                           .FirstOrDefaultAsync(b => b.BookingId == bookingID);
        }
    }
}