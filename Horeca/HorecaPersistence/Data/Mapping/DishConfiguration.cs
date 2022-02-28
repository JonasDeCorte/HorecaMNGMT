using Domain.Kitchen;
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
    public class DishConfiguration : EntityConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(250);
            builder.Property(x => x.Category).HasMaxLength(100);
        }
    }
}