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
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu");
            builder.HasKey(x => x.Id);  
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasMany(x => x.Dishes).WithOne().OnDelete(DeleteBehavior.Cascade); 
            
        }
    }
}
