using Horeca.MVC.Services;
using Horeca.MVC.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddAuthentication()
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Account/Unauthorized";
//        options.AccessDeniedPath = "/Account/Forbidden";
//    })
//    .AddJwtBearer(options =>
//    {
//        options.Audience = "https://localhost:7164/";
//        options.Authority = "https://localhost:7282/";
//    });

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(3);
});

builder.Services.AddHttpClient();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuCardService, MenuCardService>();
builder.Services.AddScoped<IAccountService, AccountService>();

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
