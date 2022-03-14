﻿using Horeca.Shared.Data.Entities;
using Horeca.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Models.Menus;
using Horeca.MVC.Models.Mappers;
using Horeca.Shared.Dtos.Menus;
using Horeca.MVC.Models.Dishes;

namespace Horeca.MVC.Controllers
{
    public class MenuController : Controller
    {
        private IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        public IActionResult Index()
        {
            IEnumerable<Menu> menus;
            menus = menuService.GetMenus();

            MenuListViewModel listModel = new MenuListViewModel();

            foreach (var item in menus)
            {
                MenuViewModel model = MenuMapper.MapModel(item);

                listModel.Menus.Add(model);
            }

            return View(listModel);
        }

        public IActionResult Detail(int id)
        {
            Menu menu = menuService.GetMenuById(id);
            if (menu.Name == null)
            {
                return View("NotFound");
            }

            MenuDetailViewModel model = MenuMapper.MapDetailModel(menu);

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            menuService.DeleteMenu(id);
            Thread.Sleep(200);

            return RedirectToAction(nameof(Index));
        }

        [Route("/Menu/DeleteDish/{menuId}/{id}")]
        public IActionResult DeleteDish(int menuId, int id)
        {
            if (menuId == 0 || id == null)
            {
                return View("NotFound");
            }

            DeleteDishMenuDto dish = new DeleteDishMenuDto();
            dish.MenuId = menuId;
            dish.DishId = id;

            menuService.DeleteMenuDish(dish);
            Thread.Sleep(200);

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
                Thread.Sleep(200);

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
                Thread.Sleep(200);

                return RedirectToAction("Detail", new { id = id });
            }
            else
            {
                return View(dish);
            }
        }

        public IActionResult Edit(int id)
        {
            Menu menu = menuService.GetMenuById(id);
            MenuViewModel model = MenuMapper.MapModel(menu);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(MenuViewModel menu)
        {
            if (ModelState.IsValid)
            {
                Menu result = MenuMapper.MapMenu(menu, menuService.GetMenuById(menu.Id));

                menuService.UpdateMenu(result);

                Thread.Sleep(200);
                return RedirectToAction(nameof(Detail), new { id = menu.Id });
            }
            else
            {
                return View(menu);
            }
        }
    }
}
