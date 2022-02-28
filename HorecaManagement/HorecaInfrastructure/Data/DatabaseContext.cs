using HorecaAPI.Data.Entities;
using HorecaShared.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseEntity>().AsEnumerable())
            {
                item.Entity.CreatedAt = DateTime.Now;
                item.Entity.UpdatedAt = DateTime.Now;
                item.Entity.IsEnabled = true;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Ingredient> Ingredients { get; set; }
    }
}