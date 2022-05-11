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

        [Route("/Table/CreateTables/{floorplanId}")]
        [HttpPost]
        public async Task<JsonResult> CreateTables([FromBody] FloorplanCanvasViewModel floorplan, int floorplanId)
        {
            if (floorplan == null)
            {
                return Json("Not Found");
            } 
            else
            {
                FloorplanDetailDto dto = FloorplanMapper.MapFloorplanDetailDto(floorplan, floorplanId, (int)restaurantService.GetCurrentRestaurantId());
                var response = await tableService.AddTablesFromFloorplan(dto, floorplanId);
                if (response == null)
                {
                    return Json("Not Found");
                }
                return Json(response);
            }
        }
    }
}
