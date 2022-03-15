using Horeca.Shared.Data.Entities;
using Horeca.MVC.Models.Ingredients;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Services;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Ingredients;

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
            IEnumerable<IngredientDto> ingredients = await ingredientService.GetIngredients();
            if (ingredients == null)
            {
                return View("NotFound");
            }
            IngredientListViewModel listModel = new IngredientListViewModel();
            foreach (var item in ingredients)
            {
                IngredientViewModel model = IngredientMapper.MapModel(item);
                listModel.Ingredients.Add(model);
            }

            return View(listModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            IngredientDto ingredient = await ingredientService.GetIngredientById(id);
            if (ingredient == null)
            {
                return View("NotFound");
            }

            IngredientViewModel model = IngredientMapper.MapModel(ingredient);

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            ingredientService.DeleteIngredient(id);

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
                MutateIngredientDto result = IngredientMapper.MapCreateIngredient(ingredient);

                ingredientService.AddIngredient(result);

                return RedirectToAction(nameof(Index));
            } else
            {
                return View(ingredient);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            IngredientDto ingredient = await ingredientService.GetIngredientById(id);
            IngredientViewModel model = IngredientMapper.MapModel(ingredient);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IngredientViewModel ingredient)
        {
            if (ModelState.IsValid)
            {
                MutateIngredientDto result = IngredientMapper.MapUpdateIngredient(ingredient, 
                    await ingredientService.GetIngredientById(ingredient.Id));

                ingredientService.UpdateIngredient(result);

                return RedirectToAction(nameof(Index));
            } else
            {
                return View(ingredient);
            }
        }
    }
}
