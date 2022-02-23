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
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Table");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Booking).WithOne(x => x.Table).HasForeignKey<Booking>(x => x.TableId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.FloorPlan).WithMany(x => x.Tables).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Orders).WithOne(x => x.Table).OnDelete(DeleteBehavior.Cascade);
        }
    }
}