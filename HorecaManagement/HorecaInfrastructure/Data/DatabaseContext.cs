﻿using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyGlobalFilters<IDelete>(e => e.IsEnabled);
            builder.Entity<Reservation>().HasOne(x => x.Table).WithOne(x => x.Reservation).HasForeignKey<Table>(x => x.ReservationId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseEntity>().AsEnumerable())
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.CreatedAt = DateTime.Now;
                        item.Entity.UpdatedAt = DateTime.Now;
                        item.Entity.IsEnabled = true;
                        break;

                    case EntityState.Deleted:
                        item.State = EntityState.Modified;
                        item.Entity.UpdatedAt = DateTime.Now;
                        item.Entity.IsEnabled = false;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuCard> MenuCards { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Table> Tables { get; set; }
    }
}