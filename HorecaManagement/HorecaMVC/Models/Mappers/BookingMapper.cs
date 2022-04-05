using Horeca.MVC.Models.Bookings;
using Horeca.Shared.Dtos.Bookings;

namespace Horeca.MVC.Models.Mappers
{
    public static class BookingMapper
    {
        public static BookingViewModel MapBookingModel(BookingDto bookingDto)
        {
            BookingViewModel bookingModel = new BookingViewModel
            {
                Id = bookingDto.Id,
                UserID = bookingDto.UserID,
                BookingDate = bookingDto.BookingDate,
                CheckIn = bookingDto.CheckIn,
                CheckOut = bookingDto.CheckOut,
                BookingNo = bookingDto.BookingNo,
                BookingStatus = bookingDto.BookingStatus,
                FullName = bookingDto.FullName,
                PhoneNo = bookingDto.PhoneNo,
            };
            return bookingModel;
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
