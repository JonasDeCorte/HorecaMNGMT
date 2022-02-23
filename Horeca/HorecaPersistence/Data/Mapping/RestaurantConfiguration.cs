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
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("Restaurant");
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.MenuCard).WithOne(x => x.Restaurant).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Bookings).WithOne(x => x.Restaurant).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.FloorPlans).WithOne(x => x.Restaurant).OnDelete(DeleteBehavior.Cascade);
         

        }
    }
}
