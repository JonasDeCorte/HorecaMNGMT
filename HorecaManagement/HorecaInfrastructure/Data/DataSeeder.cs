using Horeca.Shared.AuthUtils;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Horeca.Infrastructure.Data
{
    public static class DataSeeder
    {
        public const int AmountOfEachType = 15;

        public static async void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            for (int i = 1; i < AmountOfEachType; i++)
            {
                Ingredient ingredient = new()
                {
                    BaseAmount = i,
                    IngredientType = $"type: {i}",
                    Name = $"name: Ingredient {i}",
                    Unit = new Unit
                    {
                        Name = $"Unit name: {i}"
                    }
                };

                context.Ingredients.Add(ingredient);

                Dish dish = new()
                {
                    Category = $"Category {i}",
                    Description = $"Description {i}",
                    Name = $"name {i}",
                    DishType = $"DishType {i}",
                    Ingredients = new List<Ingredient>
                     {
                         ingredient
                     }
                };

                context.Dishes.Add(dish);

                Menu menu = new()
                {
                    Name = $"name {i}",
                    Description = $"Description {i}",
                    Category = $"Category {i}",
                    Dishes = new()
                    {
                        dish
                    },
                };

                context.Menus.Add(menu);

                MenuCard card = new()
                {
                    Name = $"name {i}",
                    Menus = new List<Menu> { menu },
                    Dishes = new List<Dish> { dish }
                };

                context.MenuCards.Add(card);
            }
            List<Permission> perms = new()
            {
                new Permission()
                {
                    Name = $"{nameof(ApplicationUser)}_NewUser"
                },
                new Permission()
                {
                    Name = $"{nameof(Unit)}_{Permissions.Read}"
                },
                new Permission()
                {
                    Name = $"{nameof(Unit)}_{Permissions.Create}"
                },
                new Permission()
                {
                    Name = $"{nameof(Unit)}_{Permissions.Update}"
                },
                new Permission()
                {
                    Name = $"{nameof(Unit)}_{Permissions.Delete}"
                },
                new Permission()
                {
                    Name = $"{nameof(Ingredient)}_{Permissions.Read}"
                },
                new Permission()
                {
                    Name = $"{nameof(Ingredient)}_{Permissions.Create}"
                },
                new Permission()
                {
                    Name = $"{nameof(Ingredient)}_{Permissions.Update}"
                },
                new Permission()
                {
                    Name = $"{nameof(Ingredient)}_{Permissions.Delete}"
                },
                new Permission()
                {
                    Name = $"{nameof(Dish)}_{Permissions.Read}"
                },
                new Permission()
                {
                    Name = $"{nameof(Dish)}_{Permissions.Create}"
                },
                new Permission()
                {
                    Name = $"{nameof(Dish)}_{Permissions.Update}"
                },
                new Permission()
                {
                    Name = $"{nameof(Dish)}_{Permissions.Delete}"
                },
                new Permission()
                {
                    Name = $"{nameof(Menu)}_{Permissions.Read}"
                },
                new Permission()
                {
                    Name = $"{nameof(Menu)}_{Permissions.Create}"
                },
                new Permission()
                {
                    Name = $"{nameof(Menu)}_{Permissions.Update}"
                },
                new Permission()
                {
                    Name = $"{nameof(Menu)}_{Permissions.Delete}"
                },
                new Permission()
                {
                    Name = $"{nameof(MenuCard)}_{Permissions.Read}"
                },
                new Permission()
                {
                    Name = $"{nameof(MenuCard)}_{Permissions.Create}"
                },
                new Permission()
                {
                    Name = $"{nameof(MenuCard)}_{Permissions.Update}"
                },
                new Permission()
                {
                    Name = $"{nameof(MenuCard)}_{Permissions.Delete}"
                },

                new Permission()
                {
                    Name = $"{nameof(Restaurant)}_{Permissions.Read}"
                },
                new Permission()
                {
                    Name = $"{nameof(Restaurant)}_{Permissions.Create}"
                },
                new Permission()
                {
                    Name = $"{nameof(Restaurant)}_{Permissions.Update}"
                },
                new Permission()
                {
                    Name = $"{nameof(Restaurant)}_{Permissions.Delete}"
                },
                new Permission()
                {
                    Name = $"{nameof(Permission)}_{Permissions.Read}"
                },
                new Permission()
                {
                    Name = $"{nameof(Permission)}_{Permissions.Create}"
                },
                new Permission()
                {
                    Name = $"{nameof(Permission)}_{Permissions.Update}"
                },
                new Permission()
                {
                    Name = $"{nameof(Permission)}_{Permissions.Delete}"
                },

                new Permission()
                {
                    Name = $"{nameof(ApplicationUser)}_{Permissions.Read}"
                },
                new Permission()
                {
                    Name = $"{nameof(ApplicationUser)}_{Permissions.Create}"
                },
                new Permission()
                {
                    Name = $"{nameof(ApplicationUser)}_{Permissions.Update}"
                },
                new Permission()
                {
                    Name = $"{nameof(ApplicationUser)}_{Permissions.Delete}"
                },
            };

            context.Permissions.AddRange(perms);

            ApplicationUser superAdmin = new()
            {
                Email = "SuperAdmin@gmail.com",
                UserName = "SuperAdmin",
                ExternalId = Guid.NewGuid().ToString(),
                IsEnabled = true,
                IsOwner = true,
            };
            await userManager.CreateAsync(superAdmin, "SuperAdmin123!");

            var listPermissions = context.Permissions.ToList();

            foreach (var permission in listPermissions)
            {
                var userPerm = new UserPermission
                {
                    PermissionId = permission.Id,
                    UserId = superAdmin.Id
                };
                context.UserPermissions.Add(userPerm);
            }

            ApplicationUser chef = new()
            {
                Email = "Chef@gmail.com",
                UserName = "Chef",
                IsEnabled = true,
                ExternalId = Guid.NewGuid().ToString(),
            };
            await userManager.CreateAsync(chef, "Chef123!");

            foreach (var permission in listPermissions)
            {
                if (permission.Id <= 21)
                {
                    var chefPerm = new UserPermission
                    {
                        PermissionId = permission.Id,
                        UserId = chef.Id
                    };
                    context.UserPermissions.Add(chefPerm);
                }
            }

            ApplicationUser zaal = new()
            {
                Email = "zaal@gmail.com",
                UserName = "zaal",
                IsEnabled = true,
                ExternalId = Guid.NewGuid().ToString(),
            };
            await userManager.CreateAsync(zaal, "Zaal123!");

            foreach (var permission in listPermissions)
            {
                if (permission.Id <= 21)

                {
                    var zaalPerms = new UserPermission
                    {
                        PermissionId = permission.Id,
                        UserId = zaal.Id
                    };
                    context.UserPermissions.Add(zaalPerms);
                }
            }

            for (int i = 1; i < AmountOfEachType; i++)
            {
                Restaurant restaurant = new()
                {
                    Name = $"McDonalds{i}",
                };
                restaurant.Employees.Add(zaal);
                restaurant.Employees.Add(chef);
                restaurant.Employees.Add(superAdmin);
                restaurant.MenuCards.Add(context.MenuCards.Find(i));
                context.Restaurants.Add(restaurant);

                await context.SaveChangesAsync();

                DateTime newSchedule = DateTime.Today.AddDays(1);
                RestaurantSchedule restaurantSchedule = new()
                {
                    RestaurantId = restaurant.Id,
                    ScheduleDate = newSchedule,
                    StartTime = newSchedule.AddHours(1),
                    EndTime = newSchedule.AddHours(2),
                    Capacity = 20,
                    AvailableSeat = 20,
                    Status = i % 2 == 0 ? (int)Constants.ScheduleStatus.Available : (int)Constants.ScheduleStatus.Expired,
                };
                context.RestaurantSchedules.Add(restaurantSchedule);

                Booking booking = new()
                {
                    FullName = $"Random name {i}",
                    BookingDate = DateTime.Today,
                    CheckIn = DateTime.Today.AddHours(i),
                    CheckOut = DateTime.Today.AddHours(i + 5 / 2),
                    PhoneNo = $"{Guid.NewGuid()}",
                    BookingNo = $"{Guid.NewGuid()}",
                    BookingStatus = i % 2 == 0 ? Constants.BookingStatus.PENDING : Constants.BookingStatus.EXPIRED,
                    UserId = i % 2 == 0 ? superAdmin.Id : zaal.Id,
                    User = i % 2 == 0 ? superAdmin : zaal,
                };
                context.Bookings.Add(booking);
                BookingDetail bookingDetail = new()
                {
                    BookingId = booking.Id,
                    Booking = booking,
                    Pax = i,
                    RestaurantSchedule = restaurantSchedule,
                    RestaurantScheduleId = restaurantSchedule.Id,
                };
                context.BookingDetails.Add(bookingDetail);
            }
            var bookingDetails = context.BookingDetails.ToList();
            foreach (var bookingDetail in bookingDetails)
            {
                Table table = new()
                {
                    Pax = bookingDetail.Pax,
                    RestaurantScheduleId = bookingDetail.RestaurantScheduleId,
                };
                context.Tables.Add(table);
            }
            await context.SaveChangesAsync();
        }
    }
}