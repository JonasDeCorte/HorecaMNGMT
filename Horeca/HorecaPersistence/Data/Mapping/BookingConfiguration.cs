using Domain.Restaurants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaPersistence.Data.Mapping
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Booking");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Restaurant).WithMany(x => x.Bookings).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Table).WithOne(x => x.Booking).OnDelete(DeleteBehavior.Cascade);
        }
    }
}