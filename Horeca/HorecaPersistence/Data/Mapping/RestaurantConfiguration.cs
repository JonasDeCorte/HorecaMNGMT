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
            builder.HasMany(x => x.MenuCard).WithOne(x => x.Restaurant).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Bookings).WithOne(x => x.Restaurant).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.FloorPlans).WithOne(x => x.Restaurant).OnDelete(DeleteBehavior.Restrict);
        }
    }
}