using Horeca.MVC.Controllers.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    [TypeFilter(typeof(TokenFilter))]
    public class OrderController : Controller
    {
        public OrderController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Create()
        {
            return RedirectToAction(nameof(Detail), new { id = 0 });
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