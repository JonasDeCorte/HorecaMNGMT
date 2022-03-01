using Microsoft.AspNetCore.Mvc;

namespace HorecaMVC.Controllers
{
    public class DishController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
