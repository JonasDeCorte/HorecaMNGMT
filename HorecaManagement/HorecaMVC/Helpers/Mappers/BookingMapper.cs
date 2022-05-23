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
                ScheduleId = bookingDto.ScheduleId,
                RestaurantId = bookingDto.RestaurantId,
                BookingNo = bookingDto.BookingNo,
                UserID = bookingDto.UserId,
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
                UserID = userDto.Id,
                RestaurantId = scheduleDto.RestaurantId,
                ScheduleId = scheduleDto.Id,
                BookingDate = scheduleDto.ScheduleDate,
                CheckIn = scheduleDto.StartTime,
                CheckOut = scheduleDto.EndTime,
                ScheduleCapacity = scheduleDto.Capacity
            };
        }

        public static EditBookingViewModel MapEditBookingModel(BookingDto booking)
        {
            return new EditBookingViewModel
            {
                Id = booking.Id,
                RestaurantId = booking.RestaurantId,
                ScheduleId = booking.ScheduleId,
                BookingDate = booking.BookingDate,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                FullName = booking.FullName,
                Pax = booking.Pax,
                PhoneNo = booking.PhoneNo,
            };
        }

        public static BookingHistoryViewModel MapBookingHistoryModel(BookingHistoryDto bookingHistoryDto)
        {
            BookingHistoryViewModel bookingHistoryViewModel = new();
            foreach (BookingDto bookingDto in bookingHistoryDto.BookingDetails)
            {
                BookingViewModel bookingViewModel = MapBookingModel(bookingDto);
                bookingHistoryViewModel.Bookings.Add(bookingViewModel);
            }
            return bookingHistoryViewModel;
        }

        public static BookingListViewModel MapBookingListModel(IEnumerable<BookingDto> bookings)
        {
            BookingListViewModel bookingListViewModel = new();
            foreach (BookingDto bookingDto in bookings)
            {
                bookingListViewModel.Bookings.Add(MapBookingModel(bookingDto));
            }
            return bookingListViewModel;
        }

        public static MakeBookingDto MapMakeBookingDto(CreateBookingViewModel model)
        {
            return new MakeBookingDto()
            {
                UserId = model.UserID,
                RestaurantId = model.RestaurantId,
                ScheduleId = model.ScheduleId,
                BookingDate = model.BookingDate,
                CheckIn = model.CheckIn,
                CheckOut = model.CheckOut,
                FullName = model.FullName,
                PhoneNo = model.PhoneNo,
                Pax = model.Pax,
            };
        }

        public static EditBookingDto MapEditBookingDto(EditBookingViewModel model)
        {
            return new EditBookingDto()
            {
                Id = model.Id,
                BookingDate = model.BookingDate,
                CheckIn = model.CheckIn,
                CheckOut = model.CheckOut,
                FullName = model.FullName,
                PhoneNo = model.PhoneNo,
                ScheduleId = model.ScheduleId,
                Pax = model.Pax,
            };
        }
    }
}