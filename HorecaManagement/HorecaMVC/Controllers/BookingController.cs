using Horeca.MVC.Models.Bookings;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class BookingController : Controller
    {
        private IBookingService bookingService { get; }
        private IAccountService accountService { get; }
        private IScheduleService scheduleService { get; }

        public BookingController(IBookingService bookingService, IAccountService accountService, IScheduleService scheduleService)
        {
            this.bookingService = bookingService;
            this.accountService = accountService;
            this.scheduleService = scheduleService;
        }

        public async Task<IActionResult> Index(string status = "all")
        {
            IEnumerable<BookingDto> bookings = await bookingService.GetBookingsByStatus(status);
            if (bookings == null)
            {
                return View("NotFound");
            }
            BookingListViewModel model = BookingMapper.MapBookingListModel(bookings);

            return View(model);
        }

        public async Task<IActionResult> YourBookings(string status = "all")
        {
            var currentUser = accountService.GetCurrentUser();
            BookingHistoryDto bookingHistory = await bookingService.GetBookingsByUserId(currentUser.Id, status);
            if (bookingHistory == null)
            {
                return View("NotFound");
            }
            BookingHistoryViewModel model = BookingMapper.MapBookingHistoryModel(bookingHistory);

            return View(model);
        }

        public async Task<IActionResult> Detail(string bookingNo)
        {
            BookingDto booking = await bookingService.GetBookingByNumber(bookingNo);
            if (booking == null)
            {
                return View("NotFound");
            }
            BookingDetailViewModel model = BookingMapper.MapBookingDetailModel(booking);

            return View(model);
        }

        public async Task<IActionResult> Create(int id)
        {
            var user = await accountService.GetUserByName(accountService.GetCurrentUser().Username);
            var schedule = await scheduleService.GetScheduleById(id);
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
                var response = await bookingService.AddBooking(bookingDto);
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


        [Route("/Booking/Delete/{bookingId}/{page}")]
        public async Task<IActionResult> Delete(int bookingId, string page)
        {
            var response = await bookingService.DeleteBooking(bookingId);
            if (response == null)
            {
                return View("OperationFailed");
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
