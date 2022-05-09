using Horeca.Shared.AuthUtils;
using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Horeca.Infrastructure.Data
{
    public static class DataSeeder
    {
        public const int AmountOfEachType = 15;
        private static readonly List<IEnumerable<Permission>>? listListPerms = new();

        public static async void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            #region Add Ingredients, Unit, Dishes, Menu's, MenuCards

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
                    Price = decimal.One * i * Random.Shared.Next(i, 20),
                    Category = $"Category {i}",
                    Description = $"Description {i}",
                    Name = $"name {i}",
                    DishType = $"DishType {i}",
                    DishIngredients = new List<DishIngredient>
                    {
                    }
                };

                context.Dishes.Add(dish);
                var dishingred = new DishIngredient()
                {
                    Ingredient = ingredient,
                    Dish = dish
                };
                dish.DishIngredients.Add(dishingred);

                Menu menu = new()
                {
                    Name = $"name {i}",
                    Description = $"Description {i}",
                    Category = $"Category {i}",
                    MenuDishes = new()
                    {
                    },
                };

                context.Menus.Add(menu);
                var menuDish = new MenuDish()
                {
                    Menu = menu,
                    Dish = dish
                };
                menu.MenuDishes.Add(menuDish);
                foreach (var menuDishObj in menu.MenuDishes)
                {
                    menu.Price += menuDishObj.Dish.Price;
                }
                MenuCard card = new()
                {
                    Name = $"name {i}",
                    Menus = new List<Menu> { menu },
                    Dishes = new List<Dish> { dish }
                };

                context.MenuCards.Add(card);
            }

            #endregion Add Ingredients, Unit, Dishes, Menu's, MenuCards

            #region permissions

            AddPermissions(context);

            var listPermissions = context.Permissions.ToList();

            var dishPerms = listPermissions.Where(x => x.Name.StartsWith("Dish_"));
            var unitPerms = listPermissions.Where(x => x.Name.StartsWith("Unit_"));
            var ingredientPerms = listPermissions.Where(x => x.Name.StartsWith("Ingredient_"));
            var menuPerms = listPermissions.Where(x => x.Name.StartsWith("Menu_"));
            var menuCardPerms = listPermissions.Where(x => x.Name.StartsWith("MenuCard_"));
            var restaurantPerms = listPermissions.Where(x => x.Name.StartsWith("Restaurant_"));
            var restaurantSchedulePerms = listPermissions.Where(x => x.Name.StartsWith("RestauranSchedule_"));
            var bookingPerms = listPermissions.Where(x => x.Name.StartsWith("Booking_"));
            var tablePerms = listPermissions.Where(x => x.Name.StartsWith("Table_"));
            var floorplanPerms = listPermissions.Where(x => x.Name.StartsWith("Floorplan_"));
            var permissionPerms = listPermissions.Where(x => x.Name.StartsWith("Permission_"));
            var ApplicationUserPerms = listPermissions.Where(x => x.Name.StartsWith("ApplicationUser_"));
            var OrderPerms = listPermissions.Where(x => x.Name.StartsWith("Order_"));
            var appUserRead = listPermissions.Where(x => x.Name.Equals("ApplicationUser_Read"));

            #endregion permissions

            #region ApplicationUser superAdmin

            ApplicationUser superAdmin = new()
            {
                Email = "SuperAdmin@gmail.com",
                UserName = "SuperAdmin",
                ExternalId = Guid.NewGuid().ToString(),
                IsEnabled = true,
                IsOwner = true,
            };
            await userManager.CreateAsync(superAdmin, "SuperAdmin123!");
            foreach (var permission in listPermissions)
            {
                var userPerm = new UserPermission
                {
                    PermissionId = permission.Id,
                    UserId = superAdmin.Id
                };
                context.UserPermissions.Add(userPerm);
            }

            #endregion ApplicationUser superAdmin

            #region ApplicationUser Chef

            ApplicationUser chef = new()
            {
                Email = "Chef@gmail.com",
                UserName = "Chef",
                IsEnabled = true,
                ExternalId = Guid.NewGuid().ToString(),
            };
            await userManager.CreateAsync(chef, "Chef123!");
            listListPerms.Add(unitPerms);
            listListPerms.Add(ingredientPerms);
            listListPerms.Add(dishPerms);
            listListPerms.Add(menuPerms);
            listListPerms.Add(menuCardPerms);
            listListPerms.Add(tablePerms);
            listListPerms.Add(OrderPerms);
            listListPerms.Add(appUserRead);
            listListPerms.Add(ApplicationUserPerms.Take(2));
            AddApplicationUserPermissions(context, chef, listListPerms);
            listListPerms.Clear();

            #endregion ApplicationUser Chef

            #region ApplicationUser Zaal

            ApplicationUser zaal = new()
            {
                Email = "zaal@gmail.com",
                UserName = "zaal",
                IsEnabled = true,
                ExternalId = Guid.NewGuid().ToString(),
            };
            await userManager.CreateAsync(zaal, "Zaal123!");
            listListPerms.Add(unitPerms);
            listListPerms.Add(ingredientPerms);
            listListPerms.Add(dishPerms);
            listListPerms.Add(menuPerms);
            listListPerms.Add(menuCardPerms);
            listListPerms.Add(tablePerms);
            listListPerms.Add(floorplanPerms);
            listListPerms.Add(bookingPerms);
            listListPerms.Add(restaurantSchedulePerms);
            listListPerms.Add(OrderPerms);
            listListPerms.Add(appUserRead);
            listListPerms.Add(ApplicationUserPerms.Take(2));

            AddApplicationUserPermissions(context, zaal, listListPerms);
            listListPerms.Clear();

            #endregion ApplicationUser Zaal

            #region ApplicationUser restaurantBeheerder

            ApplicationUser restaurantBeheerder = new()
            {
                Email = "restaurantBeheerder@gmail.com",
                UserName = "restaurantBeheerder",
                IsEnabled = true,
                ExternalId = Guid.NewGuid().ToString(),
            };
            await userManager.CreateAsync(restaurantBeheerder, "restaurantBeheerder123!");
            listListPerms.Add(tablePerms);
            listListPerms.Add(floorplanPerms);
            listListPerms.Add(bookingPerms);
            listListPerms.Add(restaurantPerms);
            listListPerms.Add(restaurantSchedulePerms);
            listListPerms.Add(permissionPerms);
            listListPerms.Add(ApplicationUserPerms);
            listListPerms.Add(OrderPerms);
            listListPerms.Add(appUserRead);
            listListPerms.Add(ApplicationUserPerms.Take(2));

            AddApplicationUserPermissions(context, restaurantBeheerder, listListPerms);
            listListPerms.Clear();

            #endregion ApplicationUser restaurantBeheerder

            #region Add Restaurants, Bookings, Tables, Orders, Floorplans

            for (int i = 1; i < AmountOfEachType; i++)
            {
                Restaurant restaurant = new()
                {
                    Name = $"McDonalds{i}",
                };
                restaurant.Employees.Add(new RestaurantUser
                {
                    Restaurant = restaurant,
                    User = zaal
                });
                restaurant.Employees.Add(new RestaurantUser
                {
                    Restaurant = restaurant,
                    User = chef
                });
                restaurant.Employees.Add(new RestaurantUser
                {
                    Restaurant = restaurant,
                    User = superAdmin
                });
                context.Restaurants.Add(restaurant);
                await context.SaveChangesAsync();
                context.Entry(restaurant).State = EntityState.Detached; // so we can re use it later on

                DateTime newSchedule = DateTime.Today.AddDays(1);
                Array scheduleStatus = Enum.GetValues(typeof(Constants.ScheduleStatus));
                Random randomStatus = new();
                Schedule schedule = new()
                {
                    RestaurantId = restaurant.Id,
                    ScheduleDate = newSchedule,
                    StartTime = newSchedule.AddHours(1),
                    EndTime = newSchedule.AddHours(2),
                    Capacity = 20,
                    AvailableSeat = 20,
                    Status = (Constants.ScheduleStatus)scheduleStatus.GetValue(randomStatus.Next(scheduleStatus.Length))
                };
                context.Schedules.Add(schedule);

                Booking booking = new()
                {
                    FullName = $"Random name {i}",
                    BookingDate = DateTime.Today,
                    CheckIn = DateTime.Today.AddHours(i),
                    CheckOut = DateTime.Today.AddHours(i + 5 / 2),
                    PhoneNo = "+32478194517",
                    BookingNo = $"{Guid.NewGuid()}",
                    BookingStatus = i % 2 == 0 ? Constants.BookingStatus.PENDING : Constants.BookingStatus.EXPIRED,
                    UserId = i % 2 == 0 ? superAdmin.Id : zaal.Id,
                    User = i % 2 == 0 ? superAdmin : zaal,
                    Schedule = schedule,
                    ScheduleId = schedule.Id,
                    Pax = i
                };
                context.Bookings.Add(booking);

                Floorplan floorplan = new()
                {
                    Name = $"Floorplan {i}",
                };
                context.Floorplans.Add(floorplan);
                await context.SaveChangesAsync();

                floorplan.RestaurantId = restaurant.Id;
                context.Floorplans.Update(floorplan);
                await context.SaveChangesAsync();

                Table table = new()
                {
                    FloorplanId = i,
                    ScheduleId = booking.ScheduleId,
                    Schedule = schedule,
                    BookingId = booking.Id,
                    Booking = booking,
                    Name = "Table" + i,
                    Pax = booking.Pax,
                    Seats = booking.Pax.ToString(),
                    Src = "https://euc1.posios.com/posimages/MAIN/images/tables/01-parasol-dark.png",
                    Type = "image",
                    OriginX = "1",
                    OriginY = "1",
                    Left = 1,
                    Top = 1,
                    Width = 1,
                    Height = 1,
                    ScaleX = 1,
                    ScaleY = 1,
                };
                context.Tables.Add(table);
            }
            await context.SaveChangesAsync();

            List<Table> list = context.Tables.AsNoTracking().ToList();

            foreach (var table in list)
            {
                var dish = await context.Dishes.AsNoTracking().SingleOrDefaultAsync(x => x.Id == table.Id);
                Order order = new()
                {
                    TableId = table.Id,
                    OrderState = Constants.OrderState.Begin,
                    OrderLines = new List<OrderLine>()
                    {
                        new OrderLine()
                        {
                        DishId = dish.Id,
                        Price = dish.Price,
                        Quantity = table.Id+1,
                        DishState = Constants.DishState.Waiting
                        },
                     }
                };
                var resto = await context.Restaurants.SingleOrDefaultAsync(x => x.Id == table.Id);
                resto.Orders.Add(order);
                context.Restaurants.Update(resto);
                await context.SaveChangesAsync();
            }

            await context.SaveChangesAsync();

            #endregion Add Restaurants, Bookings, Tables, Orders, Floorplans

            #region Add RestaurantId to ingredient

            for (int i = 1; i < AmountOfEachType; i++)
            {
                var ingredient = context.Ingredients.Find(i);
                var restaurant = context.Restaurants.Find(i);
                ingredient.Restaurant = restaurant;
                context.Ingredients.Update(ingredient);
            }
            await context.SaveChangesAsync();

            #endregion Add RestaurantId to ingredient

            #region Add RestaurantId to Unit

            for (int i = 1; i < AmountOfEachType; i++)
            {
                var unit = context.Units.Find(i);
                var restaurant = context.Restaurants.Find(i);
                unit.Restaurant = restaurant;
                context.Units.Update(unit);
            }
            await context.SaveChangesAsync();

            #endregion Add RestaurantId to Unit

            #region Add RestaurantId to Dish

            for (int i = 1; i < AmountOfEachType; i++)
            {
                var dish = context.Dishes.Find(i);
                var restaurant = context.Restaurants.Find(i);
                dish.Restaurant = restaurant;
                context.Dishes.Update(dish);
            }
            await context.SaveChangesAsync();

            #endregion Add RestaurantId to Dish

            #region Add RestaurantId to Menu

            for (int i = 1; i < AmountOfEachType; i++)
            {
                var menu = context.Menus.Find(i);
                var restaurant = context.Restaurants.Find(i);
                menu.Restaurant = restaurant;
                context.Menus.Update(menu);
            }
            await context.SaveChangesAsync();

            #endregion Add RestaurantId to Menu

            #region Add RestaurantId to MenuCard

            for (int i = 1; i < AmountOfEachType; i++)
            {
                var menuCard = context.MenuCards.Find(i);
                var restaurant = context.Restaurants.Find(i);
                menuCard.Restaurant = restaurant;
                context.MenuCards.Update(menuCard);
            }
            await context.SaveChangesAsync();

            #endregion Add RestaurantId to MenuCard
        }

        private static void AddApplicationUserPermissions(DatabaseContext? context, ApplicationUser applicationUser, List<IEnumerable<Permission>> listListPerms)
        {
            foreach (var item in listListPerms.SelectMany(listPerm => listPerm))
            {
                var perms = new UserPermission
                {
                    PermissionId = item.Id,
                    UserId = applicationUser.Id
                };
                context.UserPermissions.Add(perms);
            }
        }

        private static void AddPermissions(DatabaseContext? context)
        {
            List<Permission> perms = new()
            {
                new Permission()
                {
                    Name = PermissionConstants.NewUser
                },
                new Permission()
                {
                    Name = PermissionConstants.Unit_Read
                },
                new Permission()
                {
                    Name = PermissionConstants.Unit_Create
                },
                new Permission()
                {
                    Name = PermissionConstants.Unit_Update
                },
                new Permission()
                {
                    Name = PermissionConstants.Unit_Delete
                },
                new Permission()
                {
                    Name = PermissionConstants.Ingredient_Read
                },
                new Permission()
                {
                    Name = PermissionConstants.Ingredient_Create
                },
                new Permission()
                {
                    Name = PermissionConstants.Ingredient_Update
                },
                new Permission()
                {
                    Name = PermissionConstants.Ingredient_Delete
                },
                new Permission()
                {
                    Name = PermissionConstants.Dish_Read
                },
                new Permission()
                {
                    Name = PermissionConstants.Dish_Create
                },
                new Permission()
                {
                    Name = PermissionConstants.Dish_Update
                },
                new Permission()
                {
                    Name = PermissionConstants.Dish_Delete
                },
                new Permission()
                {
                    Name = PermissionConstants.Menu_Read
                },
                new Permission()
                {
                    Name = PermissionConstants.Menu_Create
                },
                new Permission()
                {
                    Name = PermissionConstants.Menu_Update
                },
                new Permission()
                {
                    Name = PermissionConstants.Menu_Delete
                },
                new Permission()
                {
                    Name = PermissionConstants.MenuCard_Read
                },
                new Permission()
                {
                    Name = PermissionConstants.MenuCard_Create
                },
                new Permission()
                {
                    Name = PermissionConstants.MenuCard_Update
                },
                new Permission()
                {
                    Name = PermissionConstants.MenuCard_Delete
                },
                new Permission()
                {
                    Name = PermissionConstants.Restaurant_Read
                },
                new Permission()
                {
                    Name = PermissionConstants.Restaurant_Create
                },
                new Permission()
                {
                    Name = PermissionConstants.Restaurant_Update
                },
                new Permission()
                {
                    Name = PermissionConstants.Restaurant_Delete
                },
                new Permission()
                {
                    Name = PermissionConstants.Schedule_Read
                },
                new Permission()
                {
                    Name = PermissionConstants.Schedule_Create
                },
                new Permission()
                {
                    Name = PermissionConstants.Schedule_Update
                },
                new Permission()
                {
                    Name = PermissionConstants.Schedule_Delete
                },
                new Permission()
                {
                    Name = PermissionConstants.Booking_Read
                },
                new Permission()
                {
                    Name = PermissionConstants.Booking_Create
                },
                new Permission()
                {
                    Name = PermissionConstants.Booking_Update
                },
                new Permission()
                {
                    Name = PermissionConstants.Booking_Delete
                },
                new Permission()
                {
                    Name = PermissionConstants.Table_Read
                },
                new Permission()
                {
                    Name = PermissionConstants.Table_Create
                },
                new Permission()
                {
                    Name = PermissionConstants.Table_Update
                },
                new Permission()
                {
                    Name = PermissionConstants.Table_Delete
                },

                new Permission()
                {
                    Name = PermissionConstants.Floorplan_Read
                },
                new Permission()
                {
                    Name = PermissionConstants.Floorplan_Create
                },
                new Permission()
                {
                    Name = PermissionConstants.Floorplan_Update
                },
                new Permission()
                {
                    Name = PermissionConstants.Floorplan_Delete
                },

                new Permission()
                {
                    Name = PermissionConstants.Order_Read
                },
                new Permission()
                {
                    Name = PermissionConstants.Order_Create
                },
                new Permission()
                {
                    Name = PermissionConstants.Order_Update
                },
                new Permission()
                {
                    Name = PermissionConstants.Order_Delete
                },

                new Permission()
                {
                    Name = PermissionConstants.Permission_Read
                },
                new Permission()
                {
                    Name = PermissionConstants.Permission_Create
                },
                new Permission()
                {
                    Name = PermissionConstants.Permission_Update
                },
                new Permission()
                {
                    Name = PermissionConstants.Permission_Delete
                },

                new Permission()
                {
                    Name = PermissionConstants.ApplicationUser_Read
                },
                new Permission()
                {
                    Name = PermissionConstants.ApplicationUser_Create
                },
                new Permission()
                {
                    Name = PermissionConstants.ApplicationUser_Update
                },
                new Permission()
                {
                    Name = PermissionConstants.ApplicationUser_Delete
                }
            };

            context.Permissions.AddRange(perms);
            context.SaveChangesAsync();
        }
    }
}