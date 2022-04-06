using Horeca.MVC.Controllers.Filters;
using Horeca.MVC.Models.Bookings;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Bookings;
using Microsoft.AspNetCore.Mvc;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.MVC.Controllers
{
    [TypeFilter(typeof(TokenFilter))]
    public class BookingController : Controller
    {
        public IBookingService bookingService { get; }

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        public async Task<IActionResult> Index(string status = "All")
        {
            IEnumerable<BookingDto> bookings = await bookingService.GetBookingsByStatus(status);
            if (bookings == null)
            {
                return View("NotFound");
            }
            BookingListViewModel model = BookingMapper.MapBookingListModel(bookings);

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

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await bookingService.DeleteBooking(id);
            if (response == null)
            {
                return View("OperationFailed");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
