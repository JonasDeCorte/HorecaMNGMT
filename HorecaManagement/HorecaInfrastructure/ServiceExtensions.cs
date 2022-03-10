using Horeca.Infrastructure.Data;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

        // For Identity
        public static IdentityBuilder AddIdentity(this IServiceCollection services)
        {
            return services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }

        // Adding Authentication
        public static AuthenticationBuilder AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
             }).AddJwtBearer(options =>// Adding Jwt Bearer
             {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidAudience = configuration["JWT:ValidAudience"],
                     ValidIssuer = configuration["JWT:ValidIssuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                 };
             });
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDatabaseContext(configuration).AddUnitOfWork();
        }
    }
}