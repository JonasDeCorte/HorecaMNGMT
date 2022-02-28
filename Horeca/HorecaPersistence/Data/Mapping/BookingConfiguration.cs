using Domain.Restaurants;
using HorecaPersistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorecaPersistence.Data.Mapping
{
    public class BookingConfiguration : EntityConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Restaurant).WithMany();
            builder.HasOne<Table>().WithMany(x => x.Bookings).HasForeignKey(x => x.TableId);
        }
    }
}