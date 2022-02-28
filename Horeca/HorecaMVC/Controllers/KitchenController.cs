using Microsoft.AspNetCore.Mvc;

namespace HorecaMVC.Controllers
{
    public class KitchenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
