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
                IngredientViewModel model = new IngredientViewModel();
                model.Id = item.Id;
                model.Name = item.Name;
                model.IngredientType = item.IngredientType;
                model.BaseAmount = item.BaseAmount;
                model.Unit = item.Unit;
                listModel.Ingredients.Add(model);
            }

            return View(listModel);
        }

        public IActionResult Detail(int? id)
        {
            Ingredient ingredient = ingredientService.Get(id);
            if (ingredient.Name == null)
            {
                return View("NotFound");
            }

            IngredientViewModel model = new IngredientViewModel();
            
            model.Name = ingredient.Name;
            model.IngredientType = ingredient.IngredientType;
            model.BaseAmount = ingredient.BaseAmount;
            model.Unit = ingredient.Unit;

            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return View("NotFound");
            }
            ingredientService.Delete(id);
            return View("NotFound");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            return View();
        }
    }
}
