using Domain.Kitchen;
using HorecaPersistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorecaPersistence.Data.Mapping
{
    public class MenuConfiguration : EntityConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasMany(x => x.Dishes).WithOne().OnDelete(DeleteBehavior.Restrict);
        }
    }
}