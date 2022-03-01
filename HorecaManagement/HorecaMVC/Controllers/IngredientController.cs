using Microsoft.AspNetCore.Mvc;

namespace HorecaMVC.Controllers
{
    public class IngredientController : Controller
    {
        public IActionResult Index()
        {
            bool isRequestingDelete = false;

            ViewBag.IsRequestingDelete = isRequestingDelete;
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
