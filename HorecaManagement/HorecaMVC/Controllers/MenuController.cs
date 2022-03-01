using Microsoft.AspNetCore.Mvc;

namespace HorecaMVC.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
