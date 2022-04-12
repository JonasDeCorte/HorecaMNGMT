using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Models.Schedules;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.RestaurantSchedules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HorecaMVC.Controllers
{
    public class RestaurantScheduleController : Controller
    {
        private readonly IScheduleService scheduleService;
        private readonly IAccountService accountService;

        public RestaurantScheduleController(IScheduleService scheduleService, IAccountService accountService)
        {
            this.scheduleService = scheduleService;
            this.accountService = accountService;
        }

        // note: returns the AVAILABLE schedules for this restaurant
        public async Task<IActionResult> Index(int restaurantId)
        {
            IEnumerable<RestaurantScheduleDto> restaurantSchedules = null;

            restaurantSchedules = await scheduleService.GetRestaurantSchedules(restaurantId);

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
            if (schedule == null)
            {
                return View(nameof(NotFound));
            }
            RestaurantScheduleDetailViewModel model = ScheduleMapper.MapRestaurantScheduleDetailModel(schedule);

            return View(model);
        }

        public ActionResult Create()
        {
            MutateRestaurantScheduleViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(MutateRestaurantScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateRestaurantScheduleDto restaurantDto = ScheduleMapper.MapMutateRestaurantScheduleDto(model);

                var response = await scheduleService.AddRestaurantSchedule(restaurantDto);
                if (response == null)
                {
                    return View("OperationFailed");
                }
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await scheduleService.DeleteRestaurantSchedule(id);
            if (response == null)
            {
                return View("OperationFailed");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}