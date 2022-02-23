using Domain.Kitchen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaPersistence.Data.Mapping
{
    public class MenuCardConfiguration : IEntityTypeConfiguration<MenuCard>
    {
        public void Configure(EntityTypeBuilder<MenuCard> builder)
        {
            builder.ToTable("MenuCard");
            builder.HasKey(x => x.Id);  
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasOne(x => x.Restaurant).WithMany(x => x.MenuCard).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Dishes).WithOne().OnDelete(DeleteBehavior.Cascade); 
            
        }
    }
}
