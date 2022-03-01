using Microsoft.AspNetCore.Mvc;

namespace HorecaMVC.Controllers
{
    public class MenuCardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
