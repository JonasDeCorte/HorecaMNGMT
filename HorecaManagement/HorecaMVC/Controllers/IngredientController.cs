using Horeca.MVC.Models.Ingredients;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Ingredients;

namespace Horeca.MVC.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IIngredientService ingredientService;
        private readonly IUnitService unitService;

        public IngredientController(IIngredientService ingredientService, IUnitService unitService)
        {
            this.ingredientService = ingredientService;
            this.unitService = unitService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<IngredientDto> ingredients = await ingredientService.GetIngredients();
            if (ingredients == null)
            {
                return View(nameof(NotFound));
            }
            IngredientListViewModel listModel = new();
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
                return View(nameof(NotFound));
            }
            IngredientViewModel model = IngredientMapper.MapModel(ingredient);

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await ingredientService.DeleteIngredient(id);
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            var units = await unitService.GetUnits();
            var model = IngredientMapper.MapCreateIngredientModel(units.ToList());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIngredientViewModel ingredient)
        {
            if (ModelState.IsValid)
            {
                MutateIngredientDto result = IngredientMapper.MapCreateIngredientDto(ingredient);
                var response = await ingredientService.AddIngredient(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(ingredient);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            IngredientDto ingredient = await ingredientService.GetIngredientById(id);
            if (ingredient == null)
            {
                return View(nameof(NotFound));
            }
            IngredientViewModel model = IngredientMapper.MapModel(ingredient);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, IngredientViewModel ingredient)
        {
            if (ModelState.IsValid)
            {
                MutateIngredientDto result = IngredientMapper.MapUpdateIngredientDto(ingredient, await ingredientService.GetIngredientById(id));
                var response = await ingredientService.UpdateIngredient(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(ingredient);
            }
        }
    }
}