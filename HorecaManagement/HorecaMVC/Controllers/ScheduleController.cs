using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Models.Schedules;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Schedules;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService scheduleService;
        private readonly IAccountService accountService;
        private readonly IBookingService bookingService;
        private readonly IRestaurantService restaurantService;

        public ScheduleController(IScheduleService scheduleService, IAccountService accountService, IBookingService bookingService, IRestaurantService restaurantService)
        {
            this.scheduleService = scheduleService;
            this.accountService = accountService;
            this.bookingService = bookingService;
            this.restaurantService = restaurantService;
        }

        public async Task<ActionResult> Detail(int id)
        {
            var schedule = await scheduleService.GetScheduleById(id, restaurantService.GetCurrentRestaurantId());
            var scheduleBookings = await bookingService.GetBookingsBySchedule(id);
            if (schedule == null || scheduleBookings == null)
            {
                return View(nameof(NotFound));
            }
            ScheduleDetailViewModel model = ScheduleMapper.MapScheduleDetailModel(schedule, scheduleBookings);

            return View(model);
        }

        public IActionResult Create(int restaurantId)
        {
            MutateScheduleViewModel model = new()
            {
                RestaurantId = restaurantId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MutateScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateScheduleDto restaurantDto = ScheduleMapper.MapMutateScheduleDto(model);

                var response = await scheduleService.AddSchedule(restaurantDto);
                if (response == null)
                {
                    return View(nameof(Create));
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
            var schedule = await scheduleService.GetScheduleById(id, restaurantService.GetCurrentRestaurantId());
            MutateScheduleViewModel model = ScheduleMapper.MapMutateScheduleModel(schedule);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MutateScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateScheduleDto restaurantDto = ScheduleMapper.MapMutateScheduleDto(model);
                var response = await scheduleService.UpdateSchedule(restaurantDto);
                if (response == null)
                {
                    return View(nameof(NotFound));
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
            var response = await scheduleService.DeleteSchedule(scheduleId);
            if (response == null)
            {
                return View(nameof(NotFound));
            }
            return RedirectToAction(nameof(Detail), "Restaurant", new { id = restaurantId });
        }
    }
}