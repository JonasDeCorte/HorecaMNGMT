using Horeca.Shared.Data.Entities;
using Horeca.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Models.MenuCards;

namespace Horeca.MVC.Controllers
{
    public class MenuCardController : Controller
    {
        private IMenuCardService menuCardService;

        public MenuCardController(IMenuCardService menuCardService)
        {
            this.menuCardService = menuCardService;
        }

        public IActionResult Index()
        {
            IEnumerable<MenuCard> menuCards;
            menuCards = menuCardService.GetMenuCards();

            MenuCardListViewModel listModel = new MenuCardListViewModel();

            foreach (var item in menuCards)
            {
                MenuCardViewModel model = MenuCardMapper.MapModel(item);

                listModel.MenuCards.Add(model);
            }

            return View(listModel);
        }

        public IActionResult Detail(int id)
        {
            MenuCard menuCard = menuCardService.GetMenuCardById(id);

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
                Thread.Sleep(200);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(menuCard);
            }
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            menuCardService.DeleteMenuCard(id);
            Thread.Sleep(200);

            return RedirectToAction(nameof(Index));
        }
    }
}
