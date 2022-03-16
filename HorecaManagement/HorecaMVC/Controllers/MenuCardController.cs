using Horeca.Shared.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Models.MenuCards;
using Horeca.MVC.Models.Dishes;
using Horeca.Shared.Dtos.MenuCards;
using Horeca.MVC.Models.Menus;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Menus;

namespace Horeca.MVC.Controllers
{
    public class MenuCardController : Controller
    {
        private IMenuCardService menuCardService;
        private IMenuService menuService;
        private IDishService dishService;

        public MenuCardController(IMenuCardService menuCardService, IMenuService menuService, IDishService dishService)
        {
            this.menuCardService = menuCardService;
            this.menuService = menuService;
            this.dishService = dishService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<MenuCardDto> menuCards = await menuCardService.GetMenuCards();

            if (menuCards == null)
            {
                return View("NotFound");
            }

            MenuCardListViewModel listModel = new MenuCardListViewModel();

            foreach (var item in menuCards)
            {
                MenuCardViewModel model = MenuCardMapper.MapModel(item);

                listModel.MenuCards.Add(model);
            }

            return View(listModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            MenuCard menuCard = await menuCardService.GetMenuCardDetailById(id);

            if (menuCard == null)
            {
                return View("NotFound");
            }

            MenuCardDetailViewModel model = MenuCardMapper.MapDetailModel(menuCard);

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new MenuCardViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(MenuCardViewModel menuCard)
        {
            if (ModelState.IsValid)
            {
                MutateMenuCardDto result = MenuCardMapper.MapMutateMenuCard(menuCard, new MenuCardDto());

                menuCardService.AddMenuCard(result);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(menuCard);
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
                MutateDishMenuCardDto result = MenuCardMapper.MapCreateDish(id, dish);

                menuCardService.AddMenuCardDish(id, result);

                return RedirectToAction("Detail", new { id = id });
            }
            else
            {
                return View(dish);
            }
        }

        public IActionResult CreateMenu(int id)
        {
            var model = new MenuViewModel();

            TempData["Id"] = id;

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateMenu(int id, MenuViewModel menu)
        {
            if (ModelState.IsValid)
            {
                MutateMenuMenuCardDto result = MenuCardMapper.MapCreateMenu(id, menu);

                menuCardService.AddMenuCardMenu(id, result);

                return RedirectToAction("Detail", new { id = id });
            }
            else
            {
                return View(menu);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            MenuCardDto menuCard = await menuCardService.GetMenuCardById(id);
            MenuCardViewModel model = MenuCardMapper.MapModel(menuCard);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenuCardViewModel menuCard)
        {
            if (ModelState.IsValid)
            {
                MutateMenuCardDto result = MenuCardMapper.MapMutateMenuCard(menuCard, 
                    await menuCardService.GetMenuCardById(menuCard.Id));
                menuCardService.UpdateMenuCard(result);

                return RedirectToAction(nameof(Detail), new { id = menuCard.Id });
            }
            else
            {
                return View(menuCard);
            }
        }

        [Route("/MenuCard/EditDish/{MenuCardId}/{DishId}")]
        public async Task<IActionResult> EditDish(int menuCardId, int dishId)
        {
            DishDto dish = await dishService.GetDishById(dishId);
            MenuCardDishViewModel model = MenuCardMapper.MapMutateMenuCardDishModel(menuCardId, dish);

            return View(model);
        }

        [Route("/MenuCard/EditDish/{MenuCardId}/{DishId}")]
        [HttpPost]
        public IActionResult EditDish(MenuCardDishViewModel dish)
        {
            if (ModelState.IsValid)
            {
                MutateDishMenuCardDto result = MenuCardMapper.MapUpdateDish(dish);
                menuCardService.UpdateMenuCardDish(result);

                return RedirectToAction("Detail", new { id = dish.MenuCardId });
            }
            else
            {
                return View(dish);
            }
        }

        [Route("/MenuCard/EditMenu/{MenuCardId}/{MenuId}")]
        public async Task<IActionResult> EditMenu(int menuCardId, int menuId)
        {
            MenuDto menu = await menuService.GetMenuById(menuId);
            MenuCardMenuViewModel model = MenuCardMapper.MapMutateMenuCardMenuModel(menuCardId, menu);

            return View(model);
        }

        [Route("/MenuCard/EditMenu/{MenuCardId}/{MenuId}")]
        [HttpPost]
        public IActionResult EditMenu(MenuCardMenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateMenuMenuCardDto result = MenuCardMapper.MapUpdateMenu(model);
                menuCardService.UpdateMenuCardMenu(result);

                return RedirectToAction("Detail", new { id = model.MenuCardId });
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Delete(int id)
        {
            menuCardService.DeleteMenuCard(id);

            return RedirectToAction(nameof(Index));
        }

        [Route("/MenuCard/DeleteDish/{menuCardId}/{id}")]
        public IActionResult DeleteDish(int menuCardId, int id)
        {
            if (menuCardId == 0 || id == 0)
            {
                return View("NotFound");
            }

            DeleteDishMenuCardDto dish = new DeleteDishMenuCardDto();
            dish.MenuCardId = menuCardId;
            dish.DishId = id;

            menuCardService.DeleteMenuCardDish(dish);

            return RedirectToAction("Detail", new { id = menuCardId });
        }

        [Route("/MenuCard/DeleteMenu/{menuCardId}/{id}")]
        public IActionResult DeleteMenu(int menuCardId, int id)
        {
            if (menuCardId == 0 || id == 0)
            {
                return View("NotFound");
            }

            DeleteMenuMenuCardDto menu = new DeleteMenuMenuCardDto();
            menu.MenuCardId = menuCardId;
            menu.MenuId = id;

            menuCardService.DeleteMenuCardMenu(menu);

            return RedirectToAction("Detail", new { id = menuCardId });
        }
    }
}