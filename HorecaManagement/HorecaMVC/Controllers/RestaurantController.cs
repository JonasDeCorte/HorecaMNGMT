using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Models.Restaurants;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Restaurants;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class RestaurantController : Controller
    {
        public IRestaurantService restaurantService { get; }
        public IAccountService accountService { get; }

        public RestaurantController(IRestaurantService restaurantService, IAccountService accountService)
        {
            this.restaurantService = restaurantService;
            this.accountService = accountService;
        }

        public async Task<IActionResult> Index(int id = 0)
        {
            IEnumerable<RestaurantDto> restaurants;
            if (id != 0)
            {
                restaurants = await restaurantService.GetRestaurantsByUser(id);
            } else
            {
                restaurants = await restaurantService.GetRestaurants();
            }
            if(restaurants == null)
            {
                return View("NotFound");
            }
            RestaurantListViewModel model = RestaurantMapper.MapRestaurantListModel(restaurants);

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var restaurant = await restaurantService.GetRestaurantById(id);
            if (restaurant == null)
            {
                return View("NotFound");
            }

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
