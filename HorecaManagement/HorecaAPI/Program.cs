using Horeca.API.Authorization;
using Horeca.API.Middleware;
using Horeca.Core;
using Horeca.Core.Services;
using Horeca.Infrastructure;
using Horeca.Infrastructure.Data;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Data.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddSwaggerGen(option =>
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
            new string[]{}
        }
   });
});
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCore();
builder.Services.AddIdentity();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Register our custom Authorization handler
builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

// Overrides the DefaultAuthorizationPolicyProvider with our own
// https://github.com/dotnet/aspnetcore/blob/main/src/Security/Authorization/Core/src/DefaultAuthorizationPolicyProvider.cs
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
builder.Services.AddScoped<IUserPermissionService, UserPermissionService>();

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

builder.Services.AddScoped<IApplicationDbContext>(x => x.GetService<DatabaseContext>()!);
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IRefreshTokenValidator, RefreshTokenValidator>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DataSeeder.Seed(app);
app.UseHttpsRedirection();
// Authentication & Authorization
app.UseAuthentication();
app.UseMiddleware<PermissionsMiddleware>();

app.UseAuthorization();
app.UseMiddleware<RequestResponseLogginMiddleware>();

app.MapControllers();

app.Run();