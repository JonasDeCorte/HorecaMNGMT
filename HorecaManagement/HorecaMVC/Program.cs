using Horeca.MVC.Services;
using Horeca.MVC.Helpers.Handlers;
using Horeca.MVC.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

builder.Services.AddTransient<HttpTokenHandler>();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("HttpMessageHandler")
    .AddHttpMessageHandler<HttpTokenHandler>();

builder.Services.AddHttpClient<IUnitService, UnitService>("HttpMessageHandler");
builder.Services.AddHttpClient<IIngredientService, IngredientService>("HttpMessageHandler");
builder.Services.AddHttpClient<IDishService, DishService>("HttpMessageHandler");
builder.Services.AddHttpClient<IMenuService, MenuService>("HttpMessageHandler");
builder.Services.AddHttpClient<IMenuCardService, MenuCardService>("HttpMessageHandler");
builder.Services.AddHttpClient<IRestaurantService, RestaurantService>("HttpMessageHandler");
builder.Services.AddHttpClient<IPermissionService, PermissionService>("HttpMessageHandler");
builder.Services.AddHttpClient<IAccountService, AccountService>("HttpMessageHandler");
builder.Services.AddHttpClient<IScheduleService, ScheduleService>("HttpMessageHandler");
builder.Services.AddHttpClient<IBookingService, BookingService>("HttpMessageHandler");
builder.Services.AddHttpClient<IOrderService, OrderService>("HttpMessageHandler");

builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();