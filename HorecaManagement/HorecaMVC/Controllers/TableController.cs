using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Models.Floorplans;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Floorplans;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableService tableService;
        private readonly IRestaurantService restaurantService;

        public TableController(ITableService tableService, IRestaurantService restaurantService)
        {
            this.tableService = tableService;
            this.restaurantService = restaurantService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTables([FromBody] FloorplanCanvasViewModel floorplan)
        {
            if (floorplan == null)
            {
                Console.WriteLine("Your floorplan viewmodel is null.");
                return Json("");
            } else
            {
                foreach (var table in floorplan.Objects)
                {
                    Console.WriteLine(table.Name + " " + table.originX);
                }
                FloorplanDetailDto dto = FloorplanMapper.MapFloorplanDetailDto(floorplan, 1, (int)restaurantService.GetCurrentRestaurantId());
                var response = await tableService.AddTablesFromFloorplan(dto);
                if (response == null)
                {
                    return Json("");
                }
                return Json(floorplan);
            }
        }
    }
}
