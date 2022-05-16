using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Models.Floorplans;
using Horeca.MVC.Models.Tables;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Floorplans;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableService tableService;
        private readonly IRestaurantService restaurantService;
        private readonly IFloorplanService floorplanService;
        private readonly IOrderService orderService;

        public TableController(ITableService tableService, IRestaurantService restaurantService, IFloorplanService floorplanService, IOrderService orderService)
        {
            this.tableService = tableService;
            this.restaurantService = restaurantService;
            this.floorplanService = floorplanService;
            this.orderService = orderService;
        }

        [Route("/Table/Detail/{tableId}/{floorplanId}")]
        public async Task<IActionResult> Detail(int tableId, int floorplanId)
        {
            var table = await tableService.GetTableById(tableId, floorplanId);
            var orders = await orderService.GetOrderLinesByTableId(tableId);
            if (table == null || orders == null)
            {
                return View(nameof(NotFound));
            }
            TableDetailViewModel model = TableMapper.MapTableDetailModel(table, orders);

            return View(model);
        }

        [Route("/Table/CreateTables/{floorplanId}")]
        [HttpPost]
        public async Task<JsonResult> CreateTables([FromBody] FloorplanCanvasViewModel floorplan, int floorplanId)
        {
            if (floorplan == null)
            {
                return Json(nameof(NotFound));
            } 
            else
            {
                FloorplanDetailDto oldFloorplanDto = await floorplanService.GetFloorplanById(floorplanId);
                if (oldFloorplanDto.Tables.Any())
                {
                    foreach(var table in oldFloorplanDto.Tables)
                    {
                        var res = await tableService.DeleteTable(table.Id);
                        if (res == null)
                        {
                            return Json(nameof(NotFound));
                        }
                    }
                }

                FloorplanDetailDto newFloorplanDto = FloorplanMapper.MapFloorplanDetailDto(floorplan, floorplanId, (int)restaurantService.GetCurrentRestaurantId());
                var response = await tableService.AddTablesFromFloorplan(newFloorplanDto, floorplanId);
                if (response == null)
                {
                    return Json(nameof(NotFound));
                }

                return Json(Url.Action("Edit", "Floorplan", new { id = floorplanId }));
            }
        }
    }
}
