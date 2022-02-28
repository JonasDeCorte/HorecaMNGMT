using Horeca.Infrastructure.Data;
using HorecaShared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Horeca.Infrastructure
{
    public static class ServiceExtensions
    {
        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            return services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(connectionString));
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDatabaseContext(configuration).AddUnitOfWork();
        }
    }
}