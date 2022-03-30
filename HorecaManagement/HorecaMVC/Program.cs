using Horeca.MVC.Controllers.Filters;
using Horeca.MVC.Services;
using Horeca.MVC.Services.Interfaces;
using HorecaMVC.Services.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<HttpHandler>();
builder.Services.AddHttpClient("HttpMessageHandler")
    .AddHttpMessageHandler<HttpHandler>();
builder.Services.AddHttpClient<IDishService, DishService>("HttpMessageHandler");
builder.Services.AddHttpClient<IIngredientService, IngredientService>("HttpMessageHandler");
builder.Services.AddHttpClient<IMenuService, MenuService>("HttpMessageHandler");
builder.Services.AddHttpClient<IMenuCardService, MenuCardService>("HttpMessageHandler");
builder.Services.AddHttpClient<IAccountService, AccountService>("HttpMessageHandler");

builder.Services.AddHttpClient();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<TokenFilter>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
