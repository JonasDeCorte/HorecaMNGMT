using Horeca.MVC.Models.Mappers;
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

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
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

        public async Task<IActionResult> Create(int tableId)
        {
            CreateOrderViewModel model = new CreateOrderViewModel()
            {
                TableId = tableId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            MutateOrderDto dto = OrderMapper.MapCreateOrderDto(model);
            var response = await orderService.AddOrder(dto);
            if (response == null)
            {
                return View("OperationFailed");
            }
            return View();
        }

        [Route("/Order/{restaurantId}/{orderId}/Prepare/{orderLineId}")]
        public async Task<IActionResult> PrepareOrderLine(int restaurantId, int orderId, int orderLineId)
        {
            var response = await orderService.PrepareOrderLine(restaurantId, orderId, orderLineId);
            if (response == null)
            {
                return View("OperationFailed");
            }
            return View(nameof(Index));
        }

        [Route("/Order/{restaurantId}/{orderId}/Ready/{orderLineId}")]
        public async Task<IActionResult> ReadyOrderLine(int restaurantId, int orderId, int orderLineId)
        {
            var response = await orderService.ReadyOrderLine(restaurantId, orderId, orderLineId);
            if (response == null)
            {
                return View("OperationFailed");
            }
            return View(nameof(Index));
        }

        [Route("/Order/{restaurantId}/{orderId}/Deliver")]
        public async Task<IActionResult> DeliverOrder(int restaurantId, int orderId)
        {
            var response = await orderService.DeliverOrder(restaurantId, orderId);
            if (response == null)
            {
                return View("OperationFailed");
            }
            return View(nameof(Index));
        }
    }
}