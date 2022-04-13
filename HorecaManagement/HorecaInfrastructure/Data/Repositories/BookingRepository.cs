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

        public async Task<Booking> GetBookingByID(int bookingID)
        {
            return await context.Bookings
                                         .Include(b => b.User)
                                         .FirstOrDefaultAsync(b => b.Id == bookingID);
        }

        public async Task<Booking> GetByNumber(string bookingNo)
        {
            return await context.Bookings
                                        .Include(b => b.User).FirstOrDefaultAsync(b => b.BookingNo == bookingNo);
        }

        public async Task<Booking> Add(Booking booking)
        {
            Booking newBooking = booking;

            using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    newBooking.BookingStatus = Constants.BookingStatus.COMPLETE;

                    context.Bookings.Add(newBooking);
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
            return newBooking;
        }
    }
}