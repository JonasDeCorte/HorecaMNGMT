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
    public class FloorPlanConfiguration : IEntityTypeConfiguration<FloorPlan>
    {
        public void Configure(EntityTypeBuilder<FloorPlan> builder)
        {
            builder.ToTable("FloorPlan");
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Restaurant).WithMany(x => x.FloorPlans).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Tables).WithOne(x => x.FloorPlan).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
