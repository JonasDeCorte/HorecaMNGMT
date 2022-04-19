using Horeca.MVC.Models.Bookings;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.Bookings;
using Horeca.Shared.Dtos.Schedules;

namespace Horeca.MVC.Helpers.Mappers
{
    public static class BookingMapper
    {
        public static BookingViewModel MapBookingModel(BookingDto bookingDto)
        {
            return new BookingViewModel
            {
                Id = bookingDto.Id,
                UserID = bookingDto.UserID,
                BookingNo = bookingDto.BookingNo,
                BookingDate = bookingDto.BookingDate,
                CheckIn = bookingDto.CheckIn,
                CheckOut = bookingDto.CheckOut,
                BookingStatus = bookingDto.BookingStatus
            };
        }
        public static BookingViewModel MapBookingModel(BookingDetailOnlyBookingsDto bookingDto)
        {
            return new BookingViewModel
            {
                Id = bookingDto.Booking.Id,
                UserID = bookingDto.Booking.UserID,
                BookingNo = bookingDto.Booking.BookingNo,
                BookingDate = bookingDto.Booking.BookingDate,
                CheckIn = bookingDto.Booking.CheckIn,
                CheckOut = bookingDto.Booking.CheckOut,
                BookingStatus = bookingDto.Booking.BookingStatus
            };
        }

        public static BookingDetailViewModel MapBookingDetailModel(BookingDto bookingDto)
        {
            return new BookingDetailViewModel
            {
                Id = bookingDto.Id,
                UserID = bookingDto.UserID,
                BookingNo = bookingDto.BookingNo,
                BookingDate = bookingDto.BookingDate,
                BookingStatus = bookingDto.BookingStatus,
                CheckIn = bookingDto.CheckIn,
                CheckOut = bookingDto.CheckOut,
                FullName = bookingDto.FullName,
                PhoneNo = bookingDto.PhoneNo
            };
        }

        public static CreateBookingViewModel MapCreateBookingModel(UserDto userDto, ScheduleByIdDto scheduleDto)
        {
            return new CreateBookingViewModel
            {
                Booking = new BookingInfoViewModel
                {
                    UserID = userDto.Id,
                    BookingDate = scheduleDto.ScheduleDate,
                    CheckIn = scheduleDto.StartTime,
                    CheckOut = scheduleDto.EndTime
                },
                ScheduleId = scheduleDto.Id
            };
        }

        public static BookingHistoryViewModel MapBookingHistoryModel(BookingHistoryDto bookingHistoryDto)
        {
            BookingHistoryViewModel bookingHistoryViewModel = new BookingHistoryViewModel();
            foreach (BookingDetailOnlyBookingsDto bookingDto in bookingHistoryDto.BookingDetails)
            {
                BookingViewModel bookingViewModel = MapBookingModel(bookingDto);
                bookingHistoryViewModel.Bookings.Add(bookingViewModel);
            }
            return bookingHistoryViewModel;
        }

        public static BookingListViewModel MapBookingListModel(IEnumerable<BookingDto> bookings)
        {
            BookingListViewModel bookingListViewModel = new BookingListViewModel();
            foreach (BookingDto bookingDto in bookings)
            {
                bookingListViewModel.Bookings.Add(MapBookingModel(bookingDto));
            }
            return bookingListViewModel;
        }

        public static BookingDtoInfo MapBookingInfoDto(BookingInfoViewModel model)
        {
            return new BookingDtoInfo
            {
                UserID = model.UserID,
                FullName = model.FullName,
                PhoneNo = model.PhoneNo,
                BookingDate = model.BookingDate,
                CheckIn = model.CheckIn,
                CheckOut = model.CheckOut,
            };
        }

        internal static MakeBookingDto MapMakeBookingDto(CreateBookingViewModel model)
        {
            return new MakeBookingDto
            {
                Booking = MapBookingInfoDto(model.Booking),
                Pax = model.Pax,
                ScheduleId = model.ScheduleId
            };
        }
    }
}
