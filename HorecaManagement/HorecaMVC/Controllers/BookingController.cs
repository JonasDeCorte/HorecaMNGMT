using Horeca.MVC.Controllers.Filters;
using Horeca.MVC.Models.Bookings;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class BookingController : Controller
    {
        public IBookingService BookingService { get; }
        public IAccountService AccountService { get; }
        public IScheduleService ScheduleService { get; }

        public BookingController(IBookingService bookingService, IAccountService accountService, IScheduleService scheduleService)
        {
            BookingService = bookingService;
            AccountService = accountService;
            ScheduleService = scheduleService;
        }

        public async Task<IActionResult> Index(string status = "All")
        {
            IEnumerable<BookingDto> bookings = await BookingService.GetBookingsByStatus(status);
            if (bookings == null)
            {
                return View("NotFound");
            }
            BookingListViewModel model = BookingMapper.MapBookingListModel(bookings);

            return View(model);
        }

        public async Task<IActionResult> Detail(string bookingNo)
        {
            BookingDto booking = await BookingService.GetBookingByNumber(bookingNo);
            if (booking == null)
            {
                return View("NotFound");
            }
            BookingDetailViewModel model = BookingMapper.MapBookingDetailModel(booking);

            return View(model);
        }

        public async Task<IActionResult> Create(int id)
        {
            var user = await AccountService.GetUserByName(AccountService.GetCurrentUser().Username);
            var schedule = await ScheduleService.GetRestaurantScheduleById(id);
            CreateBookingViewModel model = BookingMapper.MapCreateBookingModel(user, schedule);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                MakeBookingDto bookingDto = BookingMapper.MapMakeBookingDto(model);
                var response = await BookingService.AddBooking(bookingDto);
                if (response == null)
                {
                    return View("OperationFailed");
                }

                return RedirectToAction(nameof(Detail), "Schedule", new { id = model.ScheduleId });
            } 
            else
            {
                return View(model);
            }
        }

        public IActionResult Edit()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await BookingService.DeleteBooking(id);
            if (response == null)
            {
                return View("OperationFailed");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
