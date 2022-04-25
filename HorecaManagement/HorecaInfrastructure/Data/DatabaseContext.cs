using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Data.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyGlobalFilters<IDelete>(e => e.IsEnabled);

            builder.Entity<UserPermission>().HasOne(p => p.User)
            .WithMany(p => p.Permissions)
            .HasForeignKey(pt => pt.UserId);

            builder.Entity<UserPermission>().HasOne(p => p.Permission)
                .WithMany()
                .HasForeignKey(pt => pt.PermissionId);

            builder.Entity<DishIngredient>().HasOne(p => p.Dish)
           .WithMany(p => p.DishIngredients)
           .HasForeignKey(pt => pt.DishId);

            builder.Entity<DishIngredient>().HasOne(p => p.Ingredient)
            .WithMany(p => p.Dishes)
            .HasForeignKey(pt => pt.IngredientId);

            builder.Entity<RestaurantUser>().HasOne(p => p.User)
           .WithMany(p => p.Restaurants)
           .HasForeignKey(pt => pt.UserId);

            builder.Entity<RestaurantUser>().HasOne(p => p.Restaurant)
            .WithMany(p => p.Employees)
            .HasForeignKey(pt => pt.RestaurantId);
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

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Table> Tables { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<RestaurantUser> RestaurantUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}