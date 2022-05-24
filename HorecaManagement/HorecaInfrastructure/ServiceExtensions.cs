using Horeca.Core.Helpers;
using Horeca.Core.Services;
using Horeca.Core.Validators;
using Horeca.Infrastructure.Data;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Data.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

        public static void AddNlogConfiguration(this IServiceCollection services)
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };
            logfile.ArchiveEvery = NLog.Targets.FileArchivePeriod.Day;
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole);
            config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logfile);

            // Apply config
            NLog.LogManager.Configuration = config;
        }

        // Adding Authentication
        public static AuthenticationBuilder AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings()
            {
                AccessTokenExpirationMinutes = 3,
                AccessTokenSecret = "my_too_strong_access_secret_key",
                Audience = "https://localhost:5001",
                Issuer = "https://localhost:5001",
                RefreshTokenExpirationMinutes = 60,
                RefreshTokenSecret = "my_too_strong_refresh_secret_key"
            };
            //configuration.Bind(nameof(JwtSettings), jwtSettings);

            services.AddSingleton(jwtSettings);
            return services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessTokenSecret)),
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDatabaseContext(configuration).AddUnitOfWork();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext>(x => x.GetService<DatabaseContext>()!);
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IAccessTokenService, AccessTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IRefreshTokenValidator, RefreshTokenValidator>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
        }

        public static void AddCustomAuthorizationServices(this IServiceCollection services)
        {
            // Register our custom Authorization handler
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            // Overrides the DefaultAuthorizationPolicyProvider with our own
            // https://github.com/dotnet/aspnetcore/blob/main/src/Security/Authorization/Core/src/DefaultAuthorizationPolicyProvider.cs
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            services.AddScoped<IUserPermissionService, UserPermissionService>();
        }

        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            return services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "HORECA MANAGEMENT API", Version = "v1" });
                option.OperationFilter<SwaggerAuthorizeOperationFilter>();
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
   {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
            },
            Array.Empty<string>()
        }
   });
            });
        }
    }
}