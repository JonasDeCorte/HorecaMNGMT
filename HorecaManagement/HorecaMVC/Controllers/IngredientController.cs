using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using HorecaMVC.Models.Ingredients;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HorecaMVC.Controllers
{
    public class IngredientController : Controller
    {
        private IIngredientRepository ingredientService;

        public IngredientController(IIngredientRepository ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Ingredient> ingredients;
            ingredients = ingredientService.GetAll();

            IngredientListViewModel listModel = new IngredientListViewModel();

            foreach (var item in ingredients)
            {
                IngredientViewModel model = mapModel(item);

                listModel.Ingredients.Add(model);
            }

            return View(listModel);
        }

        public IActionResult Detail(int id)
        {
            Ingredient ingredient = ingredientService.Get(id);
            if (ingredient.Name == null)
            {
                return View("NotFound");
            }

            IngredientViewModel model = mapModel(ingredient);

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if(id == null)
            {
                return View("NotFound");
            }
            ingredientService.Delete(id);
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

                ingredientService.Add(result);

                Thread.Sleep(200);
                return RedirectToAction(nameof(Index));
            } else
            {
                return View(ingredient);
            }
        }

        public IActionResult Edit(int id)
        {
            Ingredient ingredient = ingredientService.GetIngredientIncludingUnit(id);
            IngredientViewModel model = mapModel(ingredient);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(IngredientViewModel ingredient)
        {
            if (ModelState.IsValid)
            {
                Ingredient result = ingredientService.GetIngredientIncludingUnit(ingredient.Id);

                result.Name = ingredient.Name;
                result.BaseAmount = ingredient.BaseAmount;
                result.IngredientType = ingredient.IngredientType;
                result.Unit.Name = ingredient.Unit.Name;
                result.Unit.IsEnabled = true;

                ingredientService.Update(result);

                Thread.Sleep(200);
                return RedirectToAction(nameof(Index));
            } else
            {
                return View(ingredient);
            }
        }

        public IngredientViewModel mapModel(Ingredient ingredient)
        {
            IngredientViewModel model = new IngredientViewModel();

            model.Id = ingredient.Id;
            model.Name = ingredient.Name;
            model.IngredientType = ingredient.IngredientType;
            model.BaseAmount = ingredient.BaseAmount;
            model.Unit = ingredient.Unit;

            return model;
        }
    }
}
