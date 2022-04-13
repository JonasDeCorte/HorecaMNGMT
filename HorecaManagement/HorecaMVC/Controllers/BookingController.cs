using Horeca.MVC.Controllers.Filters;
using Horeca.MVC.Models.Bookings;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    [TypeFilter(typeof(TokenFilter))]
    public class BookingController : Controller
    {
        public IBookingService bookingService { get; }
        public IAccountService accountService { get; }

        public BookingController(IBookingService bookingService, IAccountService accountService)
        {
            this.bookingService = bookingService;
            this.accountService = accountService;
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

        public async Task<IActionResult> Create()
        {
            var user = await accountService.GetUserByName(accountService.GetCurrentUser().Username);
            CreateBookingViewModel model = new CreateBookingViewModel()
            {
                Booking = new BookingDetailViewModel
                {
                    UserID = user.Id
                }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(string userId, CreateBookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                MakeBookingDto bookingDto = new MakeBookingDto
                {
                    Booking = new BookingDtoInfo
                    {
                        UserID = userId,
                        FullName = model.Booking.FullName,
                        PhoneNo = model.Booking.PhoneNo,
                        BookingDate = model.Booking.BookingDate,
                        CheckIn = model.Booking.CheckIn,
                        CheckOut = model.Booking.CheckOut,
                    },
                    Pax = model.Pax,
                    ScheduleID = model.ScheduleId
                };
                bookingService.AddBooking(bookingDto);

                return RedirectToAction(nameof(Index));
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
            var response = await bookingService.DeleteBooking(id);
            if (response == null)
            {
                return View("OperationFailed");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
