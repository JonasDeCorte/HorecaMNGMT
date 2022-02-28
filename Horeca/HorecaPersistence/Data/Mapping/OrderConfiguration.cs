using Domain.Orders;
using HorecaPersistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorecaPersistence.Data.Mapping
{
    public class OrderConfiguration : EntityConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Table).WithMany();
            builder.HasMany(x => x.Dishes).WithOne();
        }
    }
}