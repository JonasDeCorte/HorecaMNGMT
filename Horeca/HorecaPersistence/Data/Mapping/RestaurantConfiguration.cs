using Domain.Restaurants;
using HorecaPersistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorecaPersistence.Data.Mapping
{
    public class RestaurantConfiguration : EntityConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.MenuCards).WithOne();
            builder.HasMany(x => x.Bookings).WithOne();
            builder.HasMany(x => x.FloorPlans).WithOne();
        }
    }
}