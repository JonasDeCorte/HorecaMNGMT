using Horeca.Shared.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Models.Menus;
using Horeca.MVC.Models.Mappers;
using Horeca.Shared.Dtos.Menus;
using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Dishes;

namespace Horeca.MVC.Controllers
{
    public class MenuController : Controller
    {
        private IMenuService menuService;
        private IDishService dishService;

        public MenuController(IMenuService menuService, IDishService dishService)
        {
            this.menuService = menuService;
            this.dishService = dishService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<MenuDto> menus = await menuService.GetMenus();

            if (menus == null)
            {
                return View("NotFound");
            }

            MenuListViewModel listModel = new MenuListViewModel();

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
                return View("NotFound");
            }

            MenuDetailViewModel model = MenuMapper.MapDetailModel(menu);

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            menuService.DeleteMenu(id);

            return RedirectToAction(nameof(Index));
        }

        [Route("/Menu/DeleteDish/{menuId}/{id}")]
        public IActionResult DeleteDish(int menuId, int id)
        {
            DeleteDishMenuDto dish = new DeleteDishMenuDto();
            dish.MenuId = menuId;
            dish.DishId = id;

            menuService.DeleteMenuDish(dish);

            return RedirectToAction("Detail", new { id = menuId });
        }

        public IActionResult Create()
        {
            var model = new MenuViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(MenuViewModel menu)
        {
            if (ModelState.IsValid)
            {
                MutateMenuDto result = MenuMapper.MapMutateMenu(menu, new MenuDto());

                menuService.AddMenu(result);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(menu);
            }
        }
        public IActionResult CreateDish(int id)
        {
            var model = new DishViewModel();

            TempData["Id"] = id;

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateDish(int id, DishViewModel dish)
        {
            if (ModelState.IsValid)
            {
                MutateDishMenuDto result = MenuMapper.MapCreateDish(id, dish);

                menuService.AddMenuDish(id, result);

                return RedirectToAction("Detail", new { id = id });
            }
            else
            {
                return View(dish);
            }
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
                MutateMenuDto result = MenuMapper.MapMutateMenu(menu, await menuService.GetMenuById(menu.Id));

                menuService.UpdateMenu(result);

                return RedirectToAction(nameof(Detail), new { id = menu.Id });
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
        public IActionResult EditDish(MenuDishViewModel dish)
        {
            if (ModelState.IsValid)
            {
                MutateDishMenuDto result = MenuMapper.MapUpdateDish(dish);
                menuService.UpdateMenuDish(result);

                return RedirectToAction("Detail", new { id = dish.MenuId });
            }
            else
            {
                return View(dish);
            }
        }
    }
}
