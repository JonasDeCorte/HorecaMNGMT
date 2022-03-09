using Horeca.Shared.Data.Entities;
using Horeca.MVC.Models.Ingredients;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Services;

namespace Horeca.MVC.Controllers
{
    public class IngredientController : Controller
    {
        private IIngredientService ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Ingredient> ingredients = ingredientService.GetIngredients();

            IngredientListViewModel listModel = new IngredientListViewModel();

            foreach (var item in ingredients)
            {
                IngredientViewModel model = IngredientMapper.MapModel(item);

                listModel.Ingredients.Add(model);
            }

            return View(listModel);
        }

        public IActionResult Detail(int id)
        {
            Ingredient ingredient = ingredientService.GetIngredientById(id);
            if (ingredient.Name == null)
            {
                return View("NotFound");
            }

            IngredientViewModel model = IngredientMapper.MapModel(ingredient);

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if(id == null)
            {
                return View("NotFound");
            }
            ingredientService.DeleteIngredient(id);
            Thread.Sleep(200);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            var model = new IngredientViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(IngredientViewModel ingredient)
        {
            if (ModelState.IsValid)
            {
                Ingredient result = new Ingredient();
                result.Name = ingredient.Name;
                result.BaseAmount = ingredient.BaseAmount;
                result.IngredientType = ingredient.IngredientType;
                result.Unit = ingredient.Unit;

                ingredientService.AddIngredient(result);

                Thread.Sleep(200);
                return RedirectToAction(nameof(Index));
            } else
            {
                return View(ingredient);
            }
        }

        public IActionResult Edit(int id)
        {
            Ingredient ingredient = ingredientService.GetIngredientById(id);
            IngredientViewModel model = IngredientMapper.MapModel(ingredient);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(IngredientViewModel ingredient)
        {
            if (ModelState.IsValid)
            {
                Ingredient result = IngredientMapper.MapIngredient(ingredient, ingredientService.GetIngredientById(ingredient.Id));

                ingredientService.UpdateIngredient(result);

                Thread.Sleep(200);
                return RedirectToAction(nameof(Index));
            } else
            {
                return View(ingredient);
            }
        }
    }
}
