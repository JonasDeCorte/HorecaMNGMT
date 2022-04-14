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

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}