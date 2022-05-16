using Horeca.Core.Exceptions;
using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Horeca.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly DatabaseContext context;

        public BookingRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public int AdminGetPendingBookingCount()
        {
            return context.Bookings.Count(b => b.BookingStatus.Equals(Constants.BookingStatus.PENDING));
        }

        public async Task<Booking> GetBookingById(int bookingID)
        {
            return await context.Bookings.Include(b => b.User)
                                         .Include(x => x.Schedule)
                                         .FirstOrDefaultAsync(b => b.Id == bookingID);
        }

        public async Task<Booking> GetByNumber(string bookingNo)
        {
            return await context.Bookings.Include(b => b.User)
                                         .Include(x => x.Schedule)
                                         .FirstOrDefaultAsync(b => b.BookingNo == bookingNo);
        }

        public async Task<IEnumerable<Booking>> GetBookingsForRestaurantSchedule(int scheduleId)
        {
            return await context.Bookings.Include(b => b.User)
                                         .Include(b => b.Schedule)
                                         .Where(x => x.ScheduleId.Equals(scheduleId))
                                         .ToListAsync();
        }

        public async Task<Booking> Add(Booking booking)
        {
            using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var schedule = context.Schedules.Find(booking.ScheduleId);
                    if (schedule == null)
                    {
                        throw new EntityNotFoundException();
                    }
                    if (schedule.AvailableSeat < booking.Pax)
                    {
                        throw new UnAvailableSeatException();
                    }
                    schedule.AvailableSeat -= booking.Pax;
                    booking.BookingStatus = Constants.BookingStatus.COMPLETE;
                    context.Schedules.Update(schedule);
                    context.Bookings.Add(booking);
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

            return booking;
        }

        public async Task<IEnumerable<Booking>> GetDetailsByUserId(string userId, string status)
        {
            if (status.Equals("all"))
            {
                return await context.Bookings.Include(x => x.User)
                                             .Include(x => x.Schedule)
                                             .Where(b => b.User.Id == userId)
                                             .ToListAsync();
            }
            else
            {
                return await context.Bookings.Include(b => b.Schedule)
                                             .Include(x => x.User)
                                             .Where(b => b.User.Id == userId && b.BookingStatus.Equals(status))
                                             .ToListAsync();
            }
        }

        public async Task<IEnumerable<Booking>> GetAllBookings(int scheduleId)
        {
            return await context.Bookings.Include(b => b.User)
                                         .Include(b => b.Schedule)
                                         .Where(x => x.ScheduleId.Equals(scheduleId))
                                         .ToListAsync();
        }
    }
}