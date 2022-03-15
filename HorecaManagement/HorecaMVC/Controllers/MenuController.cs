using Horeca.Shared.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Models.Menus;
using Horeca.MVC.Models.Mappers;
using Horeca.Shared.Dtos.Menus;
using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Services.Interfaces;

namespace Horeca.MVC.Controllers
{
    public class MenuController : Controller
    {
        private IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Menu> menus = await menuService.GetMenus();

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
            Menu menu = await menuService.GetMenuById(id);
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
                Menu result = MenuMapper.MapMenu(menu, new Menu());

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
            Menu menu = await menuService.GetMenuById(id);
            MenuViewModel model = MenuMapper.MapModel(menu);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenuViewModel menu)
        {
            if (ModelState.IsValid)
            {
                Menu result = MenuMapper.MapMenu(menu, await menuService.GetMenuById(menu.Id));

                menuService.UpdateMenu(result);

                return RedirectToAction(nameof(Detail), new { id = menu.Id });
            }
            else
            {
                return View(menu);
            }
        }
    }
}
