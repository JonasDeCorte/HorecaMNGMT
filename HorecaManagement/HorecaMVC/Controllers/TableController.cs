using Microsoft.AspNetCore.Mvc;

namespace HorecaMVC.Controllers
{
    public class TableController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
