﻿using Horeca.MVC.Controllers.Filters;
using Horeca.MVC.Models.Bookings;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class BookingController : Controller
    {
        public IBookingService bookingService { get; }
        public IAccountService accountService { get; }
        public IScheduleService scheduleService { get; }

        public BookingController(IBookingService bookingService, IAccountService accountService, IScheduleService scheduleService)
        {
            this.bookingService = bookingService;
            this.accountService = accountService;
            this.scheduleService = scheduleService;
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

        public async Task<IActionResult> BookingHistory(string status = "All")
        {
            var currentUser = accountService.GetCurrentUser();
            IEnumerable<BookingHistoryDto> bookingHistory = await bookingService.GetBookingsByUserId(currentUser.Id, status);
            if (bookingHistory == null)
            {
                return View("NotFound");
            }
            BookingHistoryListViewModel model = BookingMapper.MapBookingHistoryListModel(bookingHistory);

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
            var schedule = await scheduleService.GetRestaurantScheduleById(id);
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

        public async Task<IActionResult> Edit(string bookingNo)
        {
            var booking = await bookingService.GetBookingByNumber(bookingNo);

            return View();
        }

        [HttpPost]
        public IActionResult Edit(CreateBookingViewModel model)
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
