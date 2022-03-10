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

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
