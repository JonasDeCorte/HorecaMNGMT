using Microsoft.AspNetCore.Mvc;

namespace HorecaMVC.Controllers
{
    public class HallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
