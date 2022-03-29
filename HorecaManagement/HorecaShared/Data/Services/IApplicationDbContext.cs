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
        public DbSet<Floorplan> Floorplans { get; set; }

        public DbSet<Table> Tables { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}