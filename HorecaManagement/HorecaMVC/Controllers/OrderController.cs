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

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Create()
        {
            return RedirectToAction(nameof(Detail), new { id = 0 });
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