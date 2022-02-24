using Domain.Restaurants;
using HorecaPersistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaPersistence.Data.Mapping
{
    public class TableConfiguration : EntityConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Orders).WithOne().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.FloorPlan).WithMany().HasForeignKey(x => x.FloorPlanId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Bookings).WithOne().HasForeignKey(x => x.TableId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}