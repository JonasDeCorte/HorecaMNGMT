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
            var schedule = context.Schedules.Find(bookingDetail.ScheduleId);

            using var transaction = context.Database.BeginTransaction();
            try
            {
                // Update Schedule AvailableSeat Value
                schedule.AvailableSeat -= bookingDetail.Pax;
                context.Schedules.Update(schedule);
                context.BookingDetails.Add(bookingDetail);

                context.Tables.Add(new Table()
                {
                    Pax = bookingDetail.Pax,
                    ScheduleId = schedule.Id,
                    Schedule = schedule,
                    BookingDetail = bookingDetail,
                    BookingDetailId = bookingDetail.Id,
                });
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

        public async Task<BookingDetail> GetDetailsByBookingId(int bookingId)
        {
            return await context.BookingDetails.Include(b => b.Schedule)
                                           .ThenInclude(b => b.Restaurant)
                                           .Include(x => x.Booking)
                                           .FirstOrDefaultAsync(b => b.BookingId.Equals(bookingId));
        }

        public async Task<IEnumerable<BookingDetail>> GetDetailsByUserId(string userId, string status = "all")
        {
            if (status.Equals("all"))
            {
                return await context.BookingDetails
                                               .Include(x => x.Booking)
                                               .ThenInclude(x => x.User)
                                               .Where(b => b.Booking.User.Id == userId)
                                               .ToListAsync();
            }
            else
            {
                return await context.BookingDetails
                                               .Include(b => b.Schedule)
                                               .Include(x => x.Booking)
                                               .ThenInclude(x => x.User)
                                               .Where(b => b.Booking.User.Id == userId && b.Booking.BookingStatus.Equals(status))
                                               .ToListAsync();
            }
        }

        public async Task<IEnumerable<BookingDetail>> GetDetailsForRestaurantSchedule(int scheduleId)
        {
            return await context.BookingDetails
                                                .Include(b => b.Schedule)
                                                .Include(x => x.Booking)
                                                .Where(x => x.ScheduleId.Equals(scheduleId))
                                                .ToListAsync();
        }
    }
}