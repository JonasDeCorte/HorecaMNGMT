using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Models.Floorplans;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Floorplans;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HorecaMVC.Controllers
{
    public class FloorplanController : Controller
    {
        private readonly IFloorplanService floorplanService;
        private readonly IRestaurantService restaurantService;

        public FloorplanController(IFloorplanService floorplanService, IRestaurantService restaurantService)
        {
            this.floorplanService = floorplanService;
            this.restaurantService = restaurantService;
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

        [Route("/Floorplan/Detail/{floorplanId}")]
        public async Task<IActionResult> Detail(int floorplanId)
        {
            var floorplan = await floorplanService.GetFloorplanById(floorplanId);
            if (floorplan == null)
            {
                return View(nameof(NotFound));
            }
            FloorplanDetailViewModel model = FloorplanMapper.MapFloorplanDetailModel(floorplan);

            GetFloorplanCanvasViewModel canvasDto = FloorplanMapper.MapFloorplanCanvasModel(floorplan);
            var json = JsonConvert.SerializeObject(canvasDto);
            model.Json = json;

            return View(model);
        }

        [Route("/Floorplan/Edit/{floorplanId}")]
        public async Task<IActionResult> Edit(int floorplanId)
        {
            var floorplan = await floorplanService.GetFloorplanById(floorplanId);
            if (floorplan == null)
            {
                return View(nameof(NotFound));
            }
            FloorplanDetailViewModel model = FloorplanMapper.MapFloorplanDetailModel(floorplan);

            GetFloorplanCanvasViewModel canvasDto = FloorplanMapper.MapFloorplanCanvasModel(floorplan);
            var json = JsonConvert.SerializeObject(canvasDto);
            model.Json = json;

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await floorplanService.DeleteFloorplan(id);
            if (response == null) 
            {
                return View(nameof(NotFound));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            var model = new FloorplanViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FloorplanViewModel floorplan)
        {
            if (ModelState.IsValid)
            {
                var restaurant = await restaurantService.GetRestaurantById((int)restaurantService.GetCurrentRestaurantId());
                MutateFloorplanDto result = FloorplanMapper.MapMutateFloorplanDto(floorplan, restaurant);
                var response = await floorplanService.AddFloorplan(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(floorplan);
            }
        }


    }
}
