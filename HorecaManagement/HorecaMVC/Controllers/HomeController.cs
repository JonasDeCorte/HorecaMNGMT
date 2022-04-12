using Horeca.MVC.Controllers.Filters;
using Horeca.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Horeca.MVC.Controllers
{
    [TypeFilter(typeof(TokenFilter))]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}