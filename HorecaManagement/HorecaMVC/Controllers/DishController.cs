using Horeca.Shared.Data.Entities;
using Horeca.MVC.Models.Dishes;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Models.Ingredients;
using Horeca.Shared.Dtos.Dishes;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Ingredients;
using Horeca.MVC.Controllers.Filters;

namespace Horeca.MVC.Controllers
{
    [TypeFilter(typeof(TokenFilter))]
    public class DishController : Controller
    {
        private IDishService dishService;
        private IIngredientService ingredientService;
        private readonly ITokenService tokenService;

        public DishController(IDishService dishService, IIngredientService ingredientService, ITokenService tokenService)
        {
            this.dishService = dishService;
            this.ingredientService = ingredientService;
            this.tokenService = tokenService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<DishDto> dishes = await dishService.GetDishes(tokenService.GetAccessToken());

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
            Dish dish = await dishService.GetDishDetailById(id, tokenService.GetAccessToken());
            if (dish == null)
            {
                return View("NotFound");
            }
            DishDetailViewModel model = DishMapper.MapDetailModel(dish);

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await dishService.DeleteDish(id, tokenService.GetAccessToken());
            if (response == null)
            {
                return View("OperationFailed");
            }

            return RedirectToAction(nameof(Index));
        }

        [Route("/Dish/DeleteIngredient/{dishId}/{id}")]
        public async Task<IActionResult> DeleteIngredient(int dishId, int id)
        {
            DeleteIngredientDishDto ingredient = new DeleteIngredientDishDto();
            ingredient.DishId = dishId;
            ingredient.IngredientId = id;

            var response = await dishService.DeleteDishIngredient(ingredient, tokenService.GetAccessToken());
            if (response == null)
            {
                return View("OperationFailed");
            }

            return RedirectToAction("Detail", new { id = dishId });
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
                var response = await dishService.AddDish(result, tokenService.GetAccessToken());
                if (response == null)
                {
                    return View("OperationFailed");
                }

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
        public async Task<IActionResult> CreateIngredient(int id, IngredientViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateIngredientByDishDto result = DishMapper.MapCreateIngredient(id, model);
                var response = await dishService.AddDishIngredient(id, result, tokenService.GetAccessToken());
                if (response == null)
                {
                    return View("OperationFailed");
                }

                return RedirectToAction("Detail", new { id = id });
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            DishDto dish = await dishService.GetDishById(id, tokenService.GetAccessToken());
            DishViewModel model = DishMapper.MapModel(dish);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DishViewModel dish)
        {
            if (ModelState.IsValid)
            {
                MutateDishDto result = DishMapper.MapMutateDish(dish, await dishService.GetDishById(dish.Id, tokenService.GetAccessToken()));
                var response = await dishService.UpdateDish(result, tokenService.GetAccessToken());
                if (response == null)
                {
                    return View("OperationFailed");
                }

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
            DishIngredientViewModel model = DishMapper.MapUpdateIngredientModel(dishId, ingredient);

            return View(model);
        }

        [Route("/Dish/EditIngredient/{dishId}/{ingredientId}")]
        [HttpPost]
        public async Task<IActionResult> EditIngredient(DishIngredientViewModel ingredient)
        {
            if (ModelState.IsValid)
            {
                MutateIngredientByDishDto result = DishMapper.MapUpdateIngredient(ingredient);
                var response = await dishService.UpdateDishIngredient(result, tokenService.GetAccessToken());
                if (response == null)
                {
                    return View("OperationFailed");
                }

                return RedirectToAction("Detail", new { id = ingredient.DishId });
            }
            else
            {
                return View(ingredient);
            }
        }
    }
}
