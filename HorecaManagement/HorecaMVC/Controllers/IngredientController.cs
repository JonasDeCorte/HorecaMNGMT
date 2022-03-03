using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using HorecaMVC.Models.Ingredients;
using Microsoft.AspNetCore.Mvc;

namespace HorecaMVC.Controllers
{
    public class IngredientController : Controller
    {
        private IIngredientRepository ingredientService;

        public IngredientController(IIngredientRepository ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<Ingredient> ingredients;
            ingredients = ingredientService.GetAll();

            IngredientListViewModel listModel = new IngredientListViewModel();

            foreach (var item in ingredients)
            {
                IngredientViewModel model = new IngredientViewModel(item);
                listModel.Ingredients.Add(model);
            }

            return View(listModel);
        }

        public IActionResult Detail(int? id)
        {
            return View();
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
