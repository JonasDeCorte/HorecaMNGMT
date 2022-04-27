using Horeca.Shared.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Models.Menus;
using Horeca.MVC.Helpers.Mappers;
using Horeca.Shared.Dtos.Menus;
using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Dishes;

namespace Horeca.MVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService menuService;
        private readonly IDishService dishService;
        private readonly IRestaurantService restaurantService;

        public MenuController(IMenuService menuService, IDishService dishService, IRestaurantService restaurantService)
        {
            this.menuService = menuService;
            this.dishService = dishService;
            this.restaurantService = restaurantService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<MenuDto> menus = await menuService.GetMenus();

            if (menus == null)
            {
                return View(nameof(NotFound));
            }

            MenuListViewModel listModel = new();

            foreach (var item in menus)
            {
                MenuViewModel model = MenuMapper.MapModel(item);

                listModel.Menus.Add(model);
            }

            return View(listModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Menu menu = await menuService.GetMenuDetailById(id);
            if (menu == null)
            {
                return View(nameof(NotFound));
            }

            MenuDetailViewModel model = MenuMapper.MapDetailModel(menu);

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await menuService.DeleteMenu(id);
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Index));
        }

        [Route("/Menu/DeleteDish/{menuId}/{id}")]
        public async Task<IActionResult> DeleteDish(int menuId, int id)
        {
            DeleteDishMenuDto dish = new();
            dish.MenuId = menuId;
            dish.DishId = id;

            var response = await menuService.DeleteMenuDish(dish);
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Detail), new { id = menuId });
        }

        public IActionResult Create()
        {
            var model = new MenuViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuViewModel menu)
        {
            if (ModelState.IsValid)
            {
                MutateMenuDto result = MenuMapper.MapMutateMenu(menu, restaurantService.GetCurrentRestaurantId());

                var response = await menuService.AddMenu(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(menu);
            }
        }

        public IActionResult CreateDish(int id)
        {
            var model = new MenuDishViewModel()
            {
                MenuId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish(MenuDishViewModel dish)
        {
            if (ModelState.IsValid)
            {
                MutateDishMenuDto result = MenuMapper.MapMutateMenuDish(dish, restaurantService.GetCurrentRestaurantId());

                var response = await menuService.AddMenuDish(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Detail), new { id = dish.MenuId });
            }
            else
            {
                return View(dish);
            }
        }

        public async Task<IActionResult> AddExistingDish(int id)
        {
            MenuDishesByIdDto menuDishesDto = await menuService.GetDishesByMenuId(id);
            IEnumerable<DishDto> dishes = await dishService.GetDishes();
            if (dishes == null || menuDishesDto == null)
            {
                return View(nameof(NotFound));
            }

            ExistingDishesViewModel model = new() { MenuId = id };
            model.Dishes = MenuMapper.MapRemainingDishesList(menuDishesDto, dishes);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddExistingDish(int id, ExistingDishesViewModel model)
        {
            MenuDishViewModel dishModel = MenuMapper.MapMenuDishModel(id, await dishService.GetDishById(model.DishId));
            MutateDishMenuDto result = MenuMapper.MapMutateMenuDish(dishModel, restaurantService.GetCurrentRestaurantId());
            var response = await menuService.AddMenuDish(result);
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Detail), new { id });
        }

        public async Task<IActionResult> Edit(int id)
        {
            MenuDto menu = await menuService.GetMenuById(id);
            MenuViewModel model = MenuMapper.MapModel(menu);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenuViewModel menu)
        {
            if (ModelState.IsValid)
            {
                MutateMenuDto result = MenuMapper.MapMutateMenu(menu, restaurantService.GetCurrentRestaurantId());

                var response = await menuService.UpdateMenu(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Detail), new { id = menu.MenuId });
            }
            else
            {
                return View(menu);
            }
        }

        [Route("/Menu/EditDish/{MenuId}/{DishId}")]
        public async Task<IActionResult> EditDish(int menuId, int dishId)
        {
            DishDto dish = await dishService.GetDishById(dishId);
            MenuDishViewModel model = MenuMapper.MapMutateDishModel(menuId, dish);

            return View(model);
        }

        [Route("/Menu/EditDish/{MenuId}/{DishId}")]
        [HttpPost]
        public async Task<IActionResult> EditDish(MenuDishViewModel dish)
        {
            if (ModelState.IsValid)
            {
                MutateDishMenuDto result = MenuMapper.MapMutateMenuDish(dish, restaurantService.GetCurrentRestaurantId());

                var response = await menuService.UpdateMenuDish(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Detail), new { id = dish.MenuId });
            }
            else
            {
                return View(dish);
            }
        }
    }
}