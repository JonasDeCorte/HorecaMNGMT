using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Models.MenuCards;
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
        private readonly IMenuCardService menuCardService;

        public RestaurantController(IRestaurantService restaurantService, IAccountService accountService, IMenuCardService menuCardService)
        {
            this.restaurantService = restaurantService;
            this.accountService = accountService;
            this.menuCardService = menuCardService;
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
                    return View(nameof(NotFound));
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
            RestaurantViewModel model = RestaurantMapper.MapMutateRestaurantModel(restaurant);

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
                    return View(nameof(NotFound));
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
                return View(nameof(NotFound));
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
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Detail), new { id = model.RestaurantId });
        }

        [Route("/Restaurant/{restaurantId}/RemoveEmployee/{employeeId}")]
        public async Task<IActionResult> RemoveEmployee(int restaurantId, string employeeId)
        {
            var response = await restaurantService.RemoveRestaurantEmployee(employeeId, restaurantId);
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Detail), new { id = restaurantId });
        }

        //public async Task<IActionResult> AddMenuCard(int restaurantId)
        //{
        //    var menuCards = await menuCardService.GetMenuCards();
        //    var restaurant = await restaurantService.GetRestaurantById(restaurantId);
        //    MutateRestaurantMenuCardViewModel model = RestaurantMapper.MapAddMenuCardModel(menuCards, restaurant);
        //    model.RestaurantId = restaurantId;
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddMenuCard(MutateRestaurantMenuCardViewModel model)
        //{
        //    var response = await restaurantService.AddRestaurantMenuCard(model.RestaurantId, model.MenuCardId);
        //    if (response == null)
        //    {
        //        return View(nameof(NotFound));
        //    }
        //    return RedirectToAction(nameof(Detail), new { id = model.RestaurantId });
        //}

        [Route("/Restaurant/{restaurantId}/RemoveMenuCard/{menuCardId}")]
        public async Task<IActionResult> RemoveMenuCard(int restaurantId, int menuCardId)
        {
            var response = await restaurantService.RemoveRestaurantMenuCard(menuCardId, restaurantId);
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Detail), new { id = restaurantId });
        }
    }
}