using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Models.Schedules;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.RestaurantSchedules;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService scheduleService;
        private readonly IAccountService accountService;
        private readonly IBookingService bookingService;

        public ScheduleController(IScheduleService scheduleService, IAccountService accountService, IBookingService bookingService)
        {
            this.scheduleService = scheduleService;
            this.accountService = accountService;
            this.bookingService = bookingService;
        }

        public async Task<IActionResult> Index(int restaurantId)
        {
            IEnumerable<RestaurantScheduleDto> restaurantSchedules = await scheduleService.GetRestaurantSchedules(restaurantId);
            if (restaurantSchedules == null)
            {
                return View(nameof(NotFound));
            }
            RestaurantScheduleListViewModel model = ScheduleMapper.MapRestaurantScheduleList(restaurantSchedules);

            return View(model);
        }

        public async Task<ActionResult> Detail(int id)
        {
            var schedule = await scheduleService.GetRestaurantScheduleById(id);
            var scheduleBookings = await bookingService.GetBookingsBySchedule(id);
            if (schedule == null || scheduleBookings == null)
            {
                return View(nameof(NotFound));
            }
            RestaurantScheduleDetailViewModel model = ScheduleMapper.MapRestaurantScheduleDetailModel(schedule, scheduleBookings);

            return View(model);
        }

        public IActionResult Create(int restaurantId)
        {
            MutateRestaurantScheduleViewModel model = new()
            {
                RestaurantId = restaurantId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MutateRestaurantScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateRestaurantScheduleDto restaurantDto = ScheduleMapper.MapMutateRestaurantScheduleDto(model);

                var response = await scheduleService.AddRestaurantSchedule(restaurantDto);
                if (response == null)
                {
                    return View("OperationFailed");
                }
                return RedirectToAction(nameof(Detail), "Restaurant", new { id = model.RestaurantId });
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var restaurantSchedule = await scheduleService.GetRestaurantScheduleById(id);
            MutateRestaurantScheduleViewModel model = ScheduleMapper.MapMutateRestaurantScheduleModel(restaurantSchedule);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MutateRestaurantScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateRestaurantScheduleDto restaurantDto = ScheduleMapper.MapMutateRestaurantScheduleDto(model);
                var response = await scheduleService.UpdateRestaurantSchedule(restaurantDto);
                if (response == null)
                {
                    return View("OperationFailed");
                }
                return RedirectToAction(nameof(Detail), "Restaurant", new { id = model.RestaurantId });
            }
            else
            {
                return View(model);
            }
        }

        [Route("/Schedule/Delete/{restaurantId}/{scheduleId}")]
        public async Task<IActionResult> Delete(int restaurantId, int scheduleId)
        {
            var response = await scheduleService.DeleteRestaurantSchedule(scheduleId);
            if (response == null)
            {
                return View("OperationFailed");
            }
            return RedirectToAction(nameof(Detail), "Restaurant", new { id = restaurantId });
        }
    }
}