using Horeca.Shared.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Shared.Data.Services
{
    public interface IApplicationDbContext
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuCard> MenuCards { get; set; }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<RestaurantSchedule> RestaurantSchedules { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<RestaurantUser> RestaurantUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Kitchen> Kitchens { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}