using Horeca.Shared.Data.Entities;
using Horeca.MVC.Models.Dishes;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Models.Ingredients;
using Horeca.Shared.Dtos.Dishes;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Ingredients;

namespace Horeca.MVC.Controllers
{
    public class DishController : Controller
    {
        private IDishService dishService;
        private IIngredientService ingredientService;

        public DishController(IDishService dishService, IIngredientService ingredientService)
        {
            this.dishService = dishService;
            this.ingredientService = ingredientService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<DishDto> dishes = await dishService.GetDishes();
            if (dishes == null)
            {
                return View("NotFound");
            }
            DishListViewModel listModel = new DishListViewModel();
            foreach (var item in dishes)
            {
                DishViewModel model = DishMapper.MapModel(item);

                listModel.Dishes.Add(model);
            }

            return View(listModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Dish dish = await dishService.GetDishDetailById(id);
            if (dish == null)
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

            return RedirectToAction(nameof(Index));
        }

        [Route("/Dish/DeleteIngredient/{dishId}/{id}")]
        public IActionResult DeleteIngredient(int dishId, int id)
        {
            if (dishId == 0 || id == null)
            {
                return View("NotFound");
            }
            DeleteIngredientDishDto ingredient = new DeleteIngredientDishDto();
            ingredient.DishId = dishId;
            ingredient.IngredientId = id;
            dishService.DeleteDishIngredient(ingredient);

            return RedirectToAction("Detail", new { id = dishId });
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
                MutateDishDto result = DishMapper.MapMutateDish(dish, new DishDto());
                dishService.AddDish(result);

                return RedirectToAction(nameof(Index));
            } else
            {
                return View(dish);
            }
        }

        public IActionResult CreateIngredient(int id)
        {
            IngredientViewModel model = new IngredientViewModel();

            TempData["Id"] = id;

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateIngredient(int id, IngredientViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateIngredientByDishDto result = DishMapper.MapCreateIngredient(id, model);
                dishService.AddDishIngredient(id, result);

                return RedirectToAction("Detail", new { id = id });
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            DishDto dish = await dishService.GetDishById(id);
            DishViewModel model = DishMapper.MapModel(dish);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DishViewModel dish)
        {
            if (ModelState.IsValid)
            {
                MutateDishDto result = DishMapper.MapMutateDish(dish, await dishService.GetDishById(dish.Id));
                dishService.UpdateDish(result);

                return RedirectToAction(nameof(Detail), new { id = dish.Id });
            }
            else
            {
                return View(dish);
            }
        }

        [Route("/Dish/EditIngredient/{dishId}/{ingredientId}")]
        public async Task<IActionResult> EditIngredient(int dishId, int ingredientId)
        {
            IngredientDto ingredient = await ingredientService.GetIngredientById(ingredientId);
            MutateIngredientViewModel model = DishMapper.MapMutateIngredientModel(dishId, ingredient);

            return View(model);
        }

        [Route("/Dish/EditIngredient/{dishId}/{ingredientId}")]
        [HttpPost]
        public IActionResult EditIngredient(MutateIngredientViewModel ingredient)
        {
            if (ModelState.IsValid)
            {
                MutateIngredientByDishDto result = DishMapper.MapUpdateIngredient(ingredient);
                dishService.UpdateDishIngredient(result);

                return RedirectToAction("Detail", new { id = ingredient.DishId });
            }
            else
            {
                return View(ingredient);
            }
        }
    }
}
