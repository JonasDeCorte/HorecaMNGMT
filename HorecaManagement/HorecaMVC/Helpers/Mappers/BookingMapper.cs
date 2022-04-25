using Horeca.MVC.Models.Bookings;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.Bookings;
using Horeca.Shared.Dtos.Schedules;

namespace Horeca.MVC.Helpers.Mappers
{
    public static class BookingMapper
    {
        public static BookingDetailViewModel MapBookingModel(BookingDto bookingDto)
        {
            return new BookingDetailViewModel
            {
                Id = bookingDto.Id,
                BookingNo = bookingDto.BookingNo,
                UserID = bookingDto.UserId,
                ScheduleId = bookingDto.ScheduleId,
                BookingDate = bookingDto.BookingDate,
                CheckIn = bookingDto.CheckIn,
                CheckOut = bookingDto.CheckOut,
                BookingStatus = bookingDto.BookingStatus,
                PhoneNo = bookingDto.PhoneNo,
                FullName = bookingDto.FullName,
                Pax = bookingDto.Pax,
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
            foreach (BookingDto bookingDto in bookingHistoryDto.BookingDetails)
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
    }
}
