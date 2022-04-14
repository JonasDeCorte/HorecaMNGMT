using FluentValidation;
using Horeca.API.Authorization;
using Horeca.API.Middleware;
using Horeca.API.PipelineBehaviours;
using Horeca.Core;
using Horeca.Core.Validators;
using Horeca.Infrastructure;
using Horeca.Infrastructure.Data;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCore();
builder.Services.AddIdentity();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddSwaggerService();
builder.Services.AddCustomAuthorizationServices();
builder.Services.RegisterServices();
builder.Services.AddNlogConfiguration();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//DataSeeder.Seed(app)
app.UseHttpsRedirection();
// Authentication & Authorization
app.UseAuthentication();
app.UseMiddleware<PermissionsMiddleware>();

app.UseAuthorization();
app.UseMiddleware<RequestResponseLogginMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();