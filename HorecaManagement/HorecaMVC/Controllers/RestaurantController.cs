using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Models.Restaurants;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Restaurants;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService restaurantService;
        private readonly IAccountService accountService;

        public RestaurantController(IRestaurantService restaurantService, IAccountService accountService)
        {
            this.restaurantService = restaurantService;
            this.accountService = accountService;
        }

        public async Task<IActionResult> Index(string id = "")
        {
            IEnumerable<RestaurantDto> restaurants = null;
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
            MutateRestaurantViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MutateRestaurantViewModel model)
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

        public async Task<IActionResult> Edit(int id)
        {
            var restaurant = await restaurantService.GetRestaurantById(id);
            MutateRestaurantViewModel model = RestaurantMapper.MapMutateRestaurantModel(restaurant);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RestaurantViewModel model)
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

        public async Task<IActionResult> AddEmployee(int restaurantId)
        {
            var employees = await accountService.GetUsers();
            var restaurant = await restaurantService.GetRestaurantById(restaurantId);
            MutateEmployeeViewModel model = RestaurantMapper.MapAddEmployeeModel(employees, restaurant);
            model.RestaurantId = restaurantId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(MutateEmployeeViewModel model)
        {
            var response = await restaurantService.AddRestaurantEmployee(model.EmployeeId, model.RestaurantId);
            if (response == null)
            {
                return View("OperationFailed");
            }

            return RedirectToAction(nameof(Detail), new { id = model.RestaurantId });
        }

        [Route("/Restaurant/{restaurantId}/RemoveEmployee/{employeeId}")]
        public async Task<IActionResult> RemoveEmployee(int restaurantId, string employeeId)
        {
            var response = await restaurantService.RemoveRestaurantEmployee(employeeId, restaurantId);
            if (response == null)
            {
                return View("OperationFailed");
            }

            return RedirectToAction(nameof(Detail), new { id = restaurantId });
        }
    }
}