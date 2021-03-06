using Horeca.MVC.Models.Bookings;
using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Bookings;
using Microsoft.AspNetCore.Mvc;
using Horeca.Shared.Dtos.Schedules;
using Horeca.Shared.Constants;

namespace Horeca.MVC.Controllers
{
    public class BookingController : Controller
    {
        private IBookingService BookingService;
        private IAccountService AccountService;
        private IScheduleService ScheduleService;
        private readonly IRestaurantService restaurantService;

        public BookingController(IBookingService bookingService, IAccountService accountService, IScheduleService scheduleService, IRestaurantService restaurantService)
        {
            this.BookingService = bookingService;
            this.AccountService = accountService;
            this.ScheduleService = scheduleService;
            this.restaurantService = restaurantService;
        }

        public async Task<IActionResult> YourBookings(string status = "all")
        {
            var currentUser = AccountService.GetCurrentUser();
            BookingHistoryDto bookingHistory = await BookingService.GetBookingsByUserId(currentUser.Id, status);
            if (bookingHistory == null)
            {
                return View("NotFound");
            }
            BookingHistoryViewModel model = BookingMapper.MapBookingHistoryModel(bookingHistory);

            return View(model);
        }

        public async Task<IActionResult> Detail(string bookingNo)
        {
            BookingDto booking = await BookingService.GetBookingByNumber(bookingNo);
            if (booking == null)
            {
                return View("NotFound");
            }
            BookingViewModel model = BookingMapper.MapBookingModel(booking);

            return View(model);
        }

        public async Task<IActionResult> Create(int id)
        {
            var user = await AccountService.GetUserByName(AccountService.GetCurrentUser().Username);
            var schedule = await ScheduleService.GetScheduleById(id, restaurantService.GetCurrentRestaurantId());
            if (user == null || schedule == null)
            {
                return View("NotFound");
            }
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
                if (response == null || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ModelState.AddModelError("Pax", ErrorConstants.ExceedSeats);
                    return View(model);
                }

                return RedirectToAction(nameof(Detail), "Schedule", new { id = model.ScheduleId });
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(string bookingNo)
        {
            BookingDto booking = await BookingService.GetBookingByNumber(bookingNo);
            ScheduleByIdDto schedule = await ScheduleService.GetScheduleById(booking.ScheduleId, booking.RestaurantId);
            if (booking == null || schedule == null)
            {
                return View(nameof(NotFound));
            }
            EditBookingViewModel model = BookingMapper.MapEditBookingModel(booking);
            model.ScheduleAvailable = schedule.AvailableSeat + booking.Pax;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                EditBookingDto bookingDto = BookingMapper.MapEditBookingDto(model);
                var response = await BookingService.UpdateBooking(bookingDto);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(YourBookings));
            }
            else
            {
                ScheduleByIdDto schedule = await ScheduleService.GetScheduleById(model.ScheduleId, model.RestaurantId);
                BookingDto booking = await BookingService.GetBookingByNumber(model.BookingNo);
                model.ScheduleAvailable = schedule.AvailableSeat + booking.Pax;
                return View(model);
            }
        }

        [Route("/Booking/Delete/{bookingId}/{page}")]
        public async Task<IActionResult> Delete(int bookingId, string page)
        {
            var response = await BookingService.DeleteBooking(bookingId);
            if (response == null)
            {
                return View(nameof(NotFound));
            }
            if (page == "BookingYourBookings")
            {
                return RedirectToAction(nameof(YourBookings));
            }
            else if (page == "BookingIndex")
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Detail), "Schedule", new { id = int.Parse(page) });
        }
    }
}