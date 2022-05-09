using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Models.Floorplans;
using Horeca.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HorecaMVC.Controllers
{
    public class FloorplanController : Controller
    {
        private readonly IFloorplanService floorplanService;

        public FloorplanController(IFloorplanService floorplanService)
        {
            this.floorplanService = floorplanService;
        }

        public async Task<IActionResult> Index()
        {
            var floorplans = await floorplanService.GetFloorplans();
            if (floorplans == null)
            {
                return View(nameof(NotFound));
            }
            FloorplanListViewModel model = FloorplanMapper.MapFloorplanListModel(floorplans);

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var floorplan = await floorplanService.GetFloorplanById(id);
            if (floorplan == null)
            {
                return View(nameof(NotFound));
            }
            FloorplanDetailViewModel model = FloorplanMapper.MapFloorplanDetailModel(floorplan);

            return View(model);
        }
    }
}
