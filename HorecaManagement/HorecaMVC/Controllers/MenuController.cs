﻿using Horeca.Shared.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Models.Menus;
using Horeca.MVC.Models.Mappers;
using Horeca.Shared.Dtos.Menus;
using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Dishes;
using Horeca.MVC.Controllers.Filters;

namespace Horeca.MVC.Controllers
{
    [TypeFilter(typeof(TokenFilter))]
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

        public async Task<IActionResult> Delete(int id)
        {
            var response = await menuService.DeleteMenu(id);
            if (response == null)
            {
                return View("OperationFailed");
            }

            return RedirectToAction(nameof(Index));
        }

        [Route("/Menu/DeleteDish/{menuId}/{id}")]
        public async Task<IActionResult> DeleteDish(int menuId, int id)
        {
            DeleteDishMenuDto dish = new DeleteDishMenuDto();
            dish.MenuId = menuId;
            dish.DishId = id;

            var response = await menuService.DeleteMenuDish(dish);
            if (response == null)
            {
                return View("OperationFailed");
            }

            return RedirectToAction("Detail", new { id = menuId });
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
                MutateMenuDto result = MenuMapper.MapMutateMenu(menu, new MenuDto());

                var response = await menuService.AddMenu(result);
                if (response == null)
                {
                    return View("OperationFailed");
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
            var model = new DishViewModel();

            TempData["Id"] = id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish(int id, DishViewModel dish)
        {
            if (ModelState.IsValid)
            {
                MutateDishMenuDto result = MenuMapper.MapMutateMenuDish(id, dish);

                var response = await menuService.AddMenuDish(id, result);
                if (response == null)
                {
                    return View("OperationFailed");
                }

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
        public async Task<IActionResult> Edit(int id, MenuViewModel menu)
        {
            if (ModelState.IsValid)
            {
                MutateMenuDto result = MenuMapper.MapMutateMenu(menu, await menuService.GetMenuById(id));

                var response = await menuService.UpdateMenu(result);
                if (response == null)
                {
                    return View("OperationFailed");
                }

                return RedirectToAction(nameof(Detail), new { id = id });
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
                MutateDishMenuDto result = MenuMapper.MapMutateMenuDish(dish.MenuId, dish);

                var response = await menuService.UpdateMenuDish(result);
                if (response == null)
                {
                    return View("OperationFailed");
                }

                return RedirectToAction("Detail", new { id = dish.MenuId });
            }
            else
            {
                return View(dish);
            }
        }
    }
}
