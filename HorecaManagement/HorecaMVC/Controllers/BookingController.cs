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

        public async Task<IActionResult> Index(string status = BookingStatus.PENDING)
        {
            IEnumerable<BookingDto> bookings = await bookingService.GetBookingsByStatus(status);
            if (bookings == null)
            {
                return View("NotFound");
            }
            BookingListViewModel model = BookingMapper.MapBookingListModel(bookings);

            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Create()
        {
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
