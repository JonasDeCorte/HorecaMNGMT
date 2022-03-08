using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Horeca.MVC.Models.Dishes;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class DishController : Controller
    {
        private IDishRepository dishService;

        public DishController(IDishRepository dishService)
        {
            this.dishService = dishService;
        }

        public IActionResult Index()
        {
            IEnumerable<Dish> dishes;
            dishes = dishService.GetAll();

            DishListViewModel listModel = new DishListViewModel();

            foreach(var item in dishes)
            {
                DishViewModel model = MapModel(item);

                listModel.Dishes.Add(model);
            }

            return View(listModel);
        }

        public DishViewModel MapModel(Dish dish)
        {
            DishViewModel model = new DishViewModel();

            model.Id = dish.Id;
            model.Name = dish.Name;
            model.Category = dish.Category;
            model.DishType = dish.DishType;

            return model;
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
