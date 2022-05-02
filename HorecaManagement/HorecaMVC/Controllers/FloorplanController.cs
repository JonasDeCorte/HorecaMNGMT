using Microsoft.AspNetCore.Mvc;

namespace HorecaMVC.Controllers
{
    public class FloorplanController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
