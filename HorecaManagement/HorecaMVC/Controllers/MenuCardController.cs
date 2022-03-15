using Horeca.Shared.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Models.MenuCards;
using Horeca.MVC.Models.Dishes;
using Horeca.Shared.Dtos.MenuCards;
using Horeca.MVC.Models.Menus;
using Horeca.MVC.Services.Interfaces;

namespace Horeca.MVC.Controllers
{
    public class MenuCardController : Controller
    {
        private IMenuCardService menuCardService;

        public MenuCardController(IMenuCardService menuCardService)
        {
            this.menuCardService = menuCardService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<MenuCard> menuCards = await menuCardService.GetMenuCards();

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
            MenuCard menuCard = await menuCardService.GetMenuCardById(id);

            if (menuCard.Name == null)
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
                MenuCard result = MenuCardMapper.MapMenuCard(menuCard, new MenuCard());

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
            MenuCard menuCard = await menuCardService.GetMenuCardById(id);
            MenuCardViewModel model = MenuCardMapper.MapModel(menuCard);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenuCardViewModel menuCard)
        {
            if (ModelState.IsValid)
            {
                MenuCard result = MenuCardMapper.MapMenuCard(menuCard, await menuCardService.GetMenuCardById(menuCard.Id));

                menuCardService.UpdateMenuCard(result);

                return RedirectToAction(nameof(Detail), new { id = menuCard.Id });
            }
            else
            {
                return View(menuCard);
            }
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

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