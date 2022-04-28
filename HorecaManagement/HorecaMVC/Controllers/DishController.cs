using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.Ingredients;
using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Ingredients;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class DishController : Controller
    {
        private readonly IDishService dishService;
        private readonly IIngredientService ingredientService;
        private readonly IRestaurantService restaurantService;
        private readonly IUnitService unitService;

        public DishController(IDishService dishService, IIngredientService ingredientService, IRestaurantService restaurantService, IUnitService unitService)
        {
            this.dishService = dishService;
            this.ingredientService = ingredientService;
            this.restaurantService = restaurantService;
            this.unitService = unitService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<DishDto> dishes = await dishService.GetDishes();
            if (dishes == null)
            {
                return View(nameof(NotFound));
            }
            DishListViewModel listModel = DishMapper.MapDishListModel(dishes);

            return View(listModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Dish dish = await dishService.GetDishDetailById(id);
            if (dish == null)
            {
                return View(nameof(NotFound));
            }
            DishDetailViewModel model = DishMapper.MapDetailModel(dish);

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await dishService.DeleteDish(id);
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Index));
        }

        [Route("/Dish/DeleteIngredient/{dishId}/{id}")]
        public async Task<IActionResult> DeleteIngredient(int dishId, int id)
        {
            DeleteIngredientDishDto ingredient = new();
            ingredient.DishId = dishId;
            ingredient.IngredientId = id;

            var response = await dishService.DeleteDishIngredient(ingredient);
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Detail), new { id = dishId });
        }

        public IActionResult Create()
        {
            var model = new DishViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DishViewModel dish)
        {
            if (ModelState.IsValid)
            {
                MutateDishDto result = DishMapper.MapMutateDish(dish, new DishDto());
                var response = await dishService.AddDish(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(dish);
            }
        }

        public async Task<IActionResult> CreateIngredient(int id)
        {
            var units = await unitService.GetUnits();
            CreateIngredientViewModel model = IngredientMapper.MapCreateIngredientModel(units.ToList());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient(int id, CreateIngredientViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateIngredientByDishDto result = DishMapper.MapCreateDishIngredientDto(id, restaurantService.GetCurrentRestaurantId(), model);
                var response = await dishService.AddDishIngredient(id, result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Detail), new { id });
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> AddExistingIngredient(int id)
        {
            DishIngredientsByIdDto dishIngredientDto = await dishService.GetIngredientsByDishId(id);
            IEnumerable<IngredientDto> ingredients = await ingredientService.GetIngredients();
            if (ingredients == null || dishIngredientDto == null)
            {
                return View(nameof(NotFound));
            }

            ExistingIngredientsViewModel model = new() { DishId = id };
            model.Ingredients = DishMapper.MapRemainingIngredientsList(dishIngredientDto, ingredients);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddExistingIngredient(int id, ExistingIngredientsViewModel model)
        {
            IngredientViewModel ingredientModel = IngredientMapper.MapModel(await ingredientService.GetIngredientById(model.IngredientId));
            MutateIngredientByDishDto result = DishMapper.MapMutateDishIngredientDto(id, restaurantService.GetCurrentRestaurantId(), ingredientModel);
            var response = await dishService.AddDishIngredient(id, result);
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Detail), new { id });
        }

        public async Task<IActionResult> Edit(int id)
        {
            DishDto dish = await dishService.GetDishById(id);
            if (dish == null)
            {
                return View(nameof(NotFound));
            }
            DishViewModel model = DishMapper.MapModel(dish);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DishViewModel dish)
        {
            if (ModelState.IsValid)
            {
                MutateDishDto result = DishMapper.MapMutateDish(dish, await dishService.GetDishById(id));
                var response = await dishService.UpdateDish(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Detail), new { id });
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
            var units = await unitService.GetUnits();
            if (ingredient == null)
            {
                return View(nameof(NotFound));
            }
            UpdateDishIngredientViewModel model = DishMapper.MapUpdateIngredientModel(dishId, ingredient, units.ToList());

            return View(model);
        }

        [Route("/Dish/EditIngredient/{dishId}/{ingredientId}")]
        [HttpPost]
        public async Task<IActionResult> EditIngredient(UpdateDishIngredientViewModel ingredient)
        {
            if (ModelState.IsValid)
            {
                MutateIngredientByDishDto result = DishMapper.MapMutateDishIngredientDto(ingredient.DishId, restaurantService.GetCurrentRestaurantId(), ingredient);
                var response = await dishService.UpdateDishIngredient(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Detail), new { id = ingredient.DishId });
            }
            else
            {
                return View(ingredient);
            }
        }
    }
}