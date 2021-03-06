using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.Orders;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IDishService dishService;
        private readonly ITableService tableService;

        public OrderController(IOrderService orderService, IDishService dishService, ITableService tableService)
        {
            this.orderService = orderService;
            this.dishService = dishService;
            this.tableService = tableService;
        }

        [Route("/Order/{restaurantId}/{state}")]
        public async Task<IActionResult> Index(int restaurantId, OrderState state = OrderState.Prepare)
        {
            List<OrderDtoDetail> orders = await orderService.GetOrdersByState(restaurantId, state);
            if (orders == null)
            {
                return View("NotFound");
            }
            OrderListViewModel model = OrderMapper.MapOrderListModel(orders, restaurantId);

            return View(model);
        }

        public async Task<IActionResult> Detail(int tableId)
        {
            List<GetOrderLinesByTableIdDto> list = await orderService.GetOrderLinesByTableId(tableId);
            if (list == null)
            {
                return View("NotFound");
            }
            return View();
        }

        public async Task<IActionResult> Create(int tableId, int floorplanId, int varyingDishes)
        {
            var dishes = await dishService.GetDishes();
            var table = await tableService.GetTableById(tableId, floorplanId);
            CreateOrderViewModel model = OrderMapper.MapCreateOrderModel(table, dishes, varyingDishes);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateOrderDto dto = OrderMapper.MapCreateOrderDto(model);
                var response = await orderService.AddOrder(dto);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }
                return RedirectToAction("Detail", "Table", new { tableId = model.TableId, floorplanId = model.FloorplanId });
            } else
            {
                var dishes = await dishService.GetDishes();
                foreach (var dish in dishes)
                {
                    OrderDishViewModel dishModel = DishMapper.MapOrderDishModel(dish);
                    model.Dishes.Add(dishModel);
                }
                return View(model);
            }
        }

        [Route("/Order/{restaurantId}/{orderId}/Prepare/{orderLineId}/{state}")]
        public async Task<IActionResult> PrepareOrderLine(int restaurantId, int orderId, int orderLineId, OrderState state = OrderState.Begin)
        {
            var response = await orderService.PrepareOrderLine(restaurantId, orderId, orderLineId);
            if (response == null)
            {
                return View(nameof(NotFound));
            }
            if (state == OrderState.Prepare)
            {
                return RedirectToAction(nameof(Index), new { restaurantId, state });
            }

            return RedirectToAction(nameof(Index), new { restaurantId, state = OrderState.Begin });
        }

        [Route("/Order/{restaurantId}/{orderId}/Ready/{orderLineId}")]
        public async Task<IActionResult> ReadyOrderLine(int restaurantId, int orderId, int orderLineId)
        {
            var response = await orderService.ReadyOrderLine(restaurantId, orderId, orderLineId);
            if (response == null)
            {
                return View(nameof(NotFound));
            }
            var deliver = await orderService.DeliverOrder(restaurantId, orderId);

            return RedirectToAction(nameof(Index), new { restaurantId, state = OrderState.Prepare });
        }

        [Route("/Order/{restaurantId}/{orderId}/Deliver")]
        public async Task<IActionResult> DeliverOrder(int restaurantId, int orderId)
        {
            var response = await orderService.DeliverOrder(restaurantId, orderId);
            if (response == null)
            {
                return View(nameof(NotFound));
            }
            return RedirectToAction(nameof(Index), new { restaurantId, state = OrderState.Prepare });
        }
    }
}