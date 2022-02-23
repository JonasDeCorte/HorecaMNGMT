using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorecaPersistence.Data.Mapping
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Table).WithMany(x => x.Orders).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Dishes).WithOne().OnDelete(DeleteBehavior.NoAction);
        }
    }
}