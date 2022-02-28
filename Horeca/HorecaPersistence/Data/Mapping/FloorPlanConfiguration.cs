using Domain.Restaurants;
using HorecaPersistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorecaPersistence.Data.Mapping
{
    public class FloorPlanConfiguration : EntityConfiguration<FloorPlan>
    {
        public void Configure(EntityTypeBuilder<FloorPlan> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Restaurant)
                .WithMany();

            builder.HasMany(x => x.Tables)
                .WithOne()
                .HasForeignKey(x => x.FloorPlanId);
        }
    }
}