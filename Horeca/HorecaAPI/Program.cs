using HorecaMVC.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using HorecaShared.Ingredients;
using HorecaServices.Ingredients;
using Microsoft.OpenApi.Models;
using HorecaShared.Dishes;
using HorecaServices.Dishes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(x => x.ToString());
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Horeca API", Version = "v1" });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
    .EnableSensitiveDataLogging(builder.Configuration.GetValue<bool>("Logging:EnableSqlParameterLogging")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews().AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssemblyContaining<IngredientDto.Mutate.Validator>();
    config.ImplicitlyValidateChildProperties = true;
});
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IDishService, DishService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();