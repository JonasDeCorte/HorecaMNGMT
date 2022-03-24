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

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCore();
builder.Services.AddIdentity();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerService();
builder.Services.AddCustomAuthorizationServices();
builder.Services.RegisterServices();
builder.Services.AddNlogConfiguration();

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