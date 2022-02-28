using Domain.Kitchen;
using HorecaPersistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorecaPersistence.Data.Mapping
{
    public class MenuCardConfiguration : EntityConfiguration<MenuCard>
    {
        public void Configure(EntityTypeBuilder<MenuCard> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasOne(x => x.Restaurant).WithMany();
            builder.HasMany(x => x.Menus).WithOne();
        }
    }
}