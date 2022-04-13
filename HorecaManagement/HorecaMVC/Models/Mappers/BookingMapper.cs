using Horeca.MVC.Models.Bookings;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.Bookings;
using Horeca.Shared.Dtos.RestaurantSchedules;

namespace Horeca.MVC.Models.Mappers
{
    public static class BookingMapper
    {
        public static BookingViewModel MapBookingModel(BookingDto bookingDto)
        {
            return new BookingViewModel
            {
                Id = bookingDto.Id,
                UserID = bookingDto.UserID,
                BookingDate = bookingDto.BookingDate,
                BookingNo = bookingDto.BookingNo,
                BookingStatus = bookingDto.BookingStatus,
                FullName = bookingDto.FullName,
            };
        }

        public static CreateBookingViewModel MapCreateBookingModel(UserDto userDto, RestaurantScheduleByIdDto scheduleDto)
        {
            return new CreateBookingViewModel
            {
                Booking = new BookingInfoViewModel
                {
                    UserID = userDto.Id,
                    CheckIn = scheduleDto.StartTime,
                    CheckOut = scheduleDto.EndTime
                },
                ScheduleId = scheduleDto.Id
            };
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

        public static BookingDetailViewModel MapBookingDetailModel(BookingDto bookingDto)
        {
            return new BookingDetailViewModel
            {
                Id = bookingDto.Id,
                UserID = bookingDto.UserID,
                BookingDate = bookingDto.BookingDate,
                BookingNo = bookingDto.BookingNo,
                BookingStatus = bookingDto.BookingStatus,
                FullName = bookingDto.FullName,
                CheckIn = bookingDto.CheckIn,
                CheckOut = bookingDto.CheckOut,
            };
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
                ScheduleID = model.ScheduleId
            };
        }
    }
}
