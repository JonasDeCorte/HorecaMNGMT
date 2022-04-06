using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Models.Restaurants;
using Horeca.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class RestaurantController : Controller
    {
        public IRestaurantService restaurantService { get; }

        public RestaurantController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        public async Task<IActionResult> Index()
        {
            var restaurants = await restaurantService.GetRestaurants();
            if(restaurants == null)
            {
                return View("NotFound");
            }
            RestaurantListViewModel model = RestaurantMapper.MapRestaurantListModel(restaurants);

            return View(model);
        }

        public IActionResult Detail()
        {
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
