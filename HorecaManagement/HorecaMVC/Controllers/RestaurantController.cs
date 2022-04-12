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

        public async Task<IActionResult> Index(string id = "")
        {
            IEnumerable<RestaurantDto> restaurants;
            if (id != "")
            {
                restaurants = await restaurantService.GetRestaurantsByUser(id);
            }
            else
            {
                restaurants = await restaurantService.GetRestaurants();
            }
            if (restaurants == null)
            {
                return View(nameof(NotFound));
            }
            RestaurantListViewModel model = RestaurantMapper.MapRestaurantListModel(restaurants);

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var restaurant = await restaurantService.GetRestaurantById(id);
            if (restaurant == null)
            {
                return View(nameof(NotFound));
            }
            RestaurantDetailViewModel model = RestaurantMapper.MapRestaurantDetailModel(restaurant);

            return View(model);
        }

        public IActionResult Create()
        {
            CreateRestaurantViewModel model = new CreateRestaurantViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRestaurantViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateRestaurantDto restaurantDto = RestaurantMapper.MapCreateRestaurantDto(model);

                var response = await restaurantService.AddRestaurant(restaurantDto);
                if (response == null)
                {
                    return View("OperationFailed");
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Update()
        {
            CreateRestaurantViewModel model = new CreateRestaurantViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RestaurantViewModel model)
        {
            if (ModelState.IsValid)
            {
                EditRestaurantDto restaurantDto = RestaurantMapper.MapEditRestaurantDto(model);
                var response = await restaurantService.UpdateRestaurant(restaurantDto);
                if (response == null)
                {
                    return View("OperationFailed");
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await restaurantService.DeleteRestaurant(id);
            if (response == null)
            {
                return View("OperationFailed");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}