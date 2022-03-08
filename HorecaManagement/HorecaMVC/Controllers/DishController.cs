using Horeca.Shared.Data.Entities;
using Horeca.MVC.Models.Dishes;
using Microsoft.AspNetCore.Mvc;
using HorecaMVC.Models.Mappers;
using Horeca.MVC.Services;

namespace Horeca.MVC.Controllers
{
    public class DishController : Controller
    {
        private IDishService dishService;

        public DishController(IDishService dishService)
        {
            this.dishService = dishService;
        }

        public IActionResult Index()
        {
            IEnumerable<Dish> dishes;
            dishes = dishService.GetDishes();

            DishListViewModel listModel = new DishListViewModel();

            foreach(var item in dishes)
            {
                DishViewModel model = DishMapper.MapModel(item);

                listModel.Dishes.Add(model);
            }

            return View(listModel);
        }

        public IActionResult Detail(int id)
        {
            Dish dish = dishService.GetDishById(id);
            if (dish.Name == null)
            {
                return View("NotFound");
            }

            DishDetailViewModel model = DishMapper.MapDetailModel(dish);

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            dishService.DeleteDish(id);
            Thread.Sleep(200);

            return RedirectToAction(nameof(Index));
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
