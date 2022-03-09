using Horeca.Shared.Data.Entities;
using Horeca.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Models.Menus;
using HorecaMVC.Models.Mappers;

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
