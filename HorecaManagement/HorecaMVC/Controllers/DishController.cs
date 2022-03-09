using Horeca.Shared.Data.Entities;
using Horeca.MVC.Models.Dishes;
using Microsoft.AspNetCore.Mvc;
using HorecaMVC.Models.Mappers;
using Horeca.MVC.Services;
using Horeca.MVC.Models.Ingredients;
using Horeca.Shared.Dtos.Dishes;

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

        [Route("/Dish/DeleteIngredient/{dishId}/{id}")]
        public IActionResult DeleteIngredient(int dishId, int id)
        {
            if (dishId == 0 || id == null)
            {
                return View("NotFound");
            }
            Console.WriteLine(dishId);

            DeleteIngredientDishDto ingredient = new DeleteIngredientDishDto();
            ingredient.DishId = dishId;
            ingredient.IngredientId = id;

            dishService.DeleteDishIngredient(ingredient);
            Thread.Sleep(200);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            var model = new DishViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(DishViewModel dish)
        {
            if (ModelState.IsValid)
            {
                Dish result = DishMapper.MapDish(dish, new Dish());

                dishService.AddDish(result);
                Thread.Sleep(200);

                return RedirectToAction(nameof(Index));
            } else
            {
                return View(dish);
            }
        }

        public IActionResult CreateIngredient(int id)
        {
            var model = new IngredientViewModel();

            TempData["Id"] = id;

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateIngredient(int id, IngredientViewModel ingredient)
        {
            if (ModelState.IsValid)
            {
                MutateIngredientByDishDto result = DishMapper.MapCreateIngredient(id, ingredient);

                dishService.AddDishIngredient(id, result);
                Thread.Sleep(200);

                return RedirectToAction("Detail", new { id = id });
            }
            else
            {
                return View(ingredient);
            }
        }

        public IActionResult Edit(int id)
        {
            Dish dish = dishService.GetDishById(id);
            DishViewModel model = DishMapper.MapModel(dish);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DishViewModel dish)
        {
            if (ModelState.IsValid)
            {
                Dish result = DishMapper.MapDish(dish, dishService.GetDishById(dish.Id));

                dishService.UpdateDish(result);

                Thread.Sleep(200);
                return RedirectToAction(nameof(Detail), new { id = dish.Id });
            }
            else
            {
                return View(dish);
            }
        }
    }
}
