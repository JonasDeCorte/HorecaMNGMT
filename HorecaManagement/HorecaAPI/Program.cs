using Horeca.API.Authorization;
using Horeca.API.Middleware;
using Horeca.API.PipelineBehaviors;
using Horeca.Core;
using Horeca.Infrastructure;
using Horeca.Infrastructure.Data;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

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
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Logging.AddEventSourceLogger();
var app = builder.Build();

//// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();
////}

//DataSeeder.Seed(app);
app.UseHttpsRedirection();
// Authentication & Authorization
app.UseAuthentication();
app.UseMiddleware<PermissionsMiddleware>();

app.UseAuthorization();
app.UseMiddleware<RequestResponseLogginMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.Run();