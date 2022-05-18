using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Models.Floorplans;
using Horeca.MVC.Models.Tables;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Floorplans;
using Horeca.Shared.Dtos.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableService tableService;
        private readonly IRestaurantService restaurantService;
        private readonly IFloorplanService floorplanService;
        private readonly IOrderService orderService;
        private readonly IDishService dishService;

        public TableController(ITableService tableService, IRestaurantService restaurantService, IFloorplanService floorplanService, IOrderService orderService,
            IDishService dishService)
        {
            this.tableService = tableService;
            this.restaurantService = restaurantService;
            this.floorplanService = floorplanService;
            this.orderService = orderService;
            this.dishService = dishService;
        }

        [Route("/Table/Detail/{tableId}/{floorplanId}")]
        public async Task<IActionResult> Detail(int tableId, int floorplanId)
        {
            var table = await tableService.GetTableById(tableId, floorplanId);
            var orders = await orderService.GetOrderLinesByTableId(tableId);
            var dishes = await dishService.GetDishes();
            if (table == null || orders == null)
            {
                return View(nameof(NotFound));
            }
            TableDetailViewModel model = TableMapper.MapTableDetailModel(table, orders, dishes.Count());

            return View(model);
        }

        public async Task<IActionResult> Edit(int tableId, int floorplanId)
        {
            TableDto table = await tableService.GetTableById(tableId, floorplanId);
            EditTableViewModel model = TableMapper.MapEditTableModel(table);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTableViewModel table)
        {
            Console.WriteLine("entered edit method");
            if (ModelState.IsValid)
            {
                Console.WriteLine("entered modelstate ");

                EditTableDto result = TableMapper.MapEditTableDto(table, table.FloorplanId);

                var response = await tableService.EditTableFromFloorplan(result, table.FloorplanId);
                if (response == null)
                {
                    Console.WriteLine("response edit method null ");

                    return View(nameof(NotFound));
                }
                Console.WriteLine("response not null edit method");

                return RedirectToAction(nameof(Detail), new { tableId = table.Id, floorplanId = table.FloorplanId });
            }
            else
            {
                return View(table);
            }
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
                    foreach (var table in oldFloorplanDto.Tables)
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