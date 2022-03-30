using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Bookings;
using Horeca.Shared.Dtos.RestaurantSchedules;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Bookings
{
    public class GetAllBookingsByUserIDQuery : IRequest<BookingHistoryDto>
    {
        public GetAllBookingsByUserIDQuery(string userId, string status)
        {
            UserId = userId;
            Status = status;
        }

        public string UserId { get; }
        public string Status { get; }
    }

    public class GetAllBookingsByUserIDQueryHandler : IRequestHandler<GetAllBookingsByUserIDQuery, BookingHistoryDto>
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IUnitOfWork repository;

        public GetAllBookingsByUserIDQueryHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<BookingHistoryDto> Handle(GetAllBookingsByUserIDQuery request, CancellationToken cancellationToken)
        {
            logger.Info("requested to return bookinghistory with request: {@req}", request);

            var bookingList = await repository.Bookings.GetByUserID(request.UserId, request.Status);
            logger.Info("bookinglist found with: {req} items", bookingList.Count());

            BookingDetailDto bookingDetailDto = null;
            BookingHistoryDto bookingHistoryDto = new()
            {
                BookingDetails = new List<BookingDetailDto>(),
                Bookings = bookingList.Select(x => new BookingDto
                {
                    BookingDate = x.BookingDate,
                    Id = x.Id,
                    BookingNo = x.BookingNo,
                    BookingStatus = x.BookingStatus,
                    CheckIn = x.CheckIn,
                    CheckOut = x.CheckOut,
                    FullName = x.FullName,
                    PhoneNo = x.PhoneNo,
                    UserID = x.UserId
                }).ToList()
            };
            foreach (var item in bookingList)
            {
                var bookingDetail = await repository.BookingDetails.GetDetailsByID(item.Id);
                bookingDetailDto = new BookingDetailDto()
                {
                    Booking = new BookingDto
                    {
                        BookingDate = bookingDetail.Booking.BookingDate,
                        Id = bookingDetail.Booking.Id,
                        BookingNo = bookingDetail.Booking.BookingNo,
                        BookingStatus = bookingDetail.Booking.BookingStatus,
                        CheckIn = bookingDetail.Booking.CheckIn,
                        CheckOut = bookingDetail.Booking.CheckOut,
                        FullName = bookingDetail.Booking.FullName,
                        PhoneNo = bookingDetail.Booking.PhoneNo,
                        UserID = bookingDetail.Booking.UserId
                    },
                    BookingID = bookingDetail.BookingId,
                    Pax = bookingDetail.Pax,
                    RestaurantSchedule = new RestaurantScheduleDto
                    {
                        AvailableSeat = bookingDetail.RestaurantSchedule.AvailableSeat,
                        Capacity = bookingDetail.RestaurantSchedule.Capacity,
                        EndTime = bookingDetail.RestaurantSchedule.EndTime,
                        RestaurantId = bookingDetail.RestaurantSchedule.RestaurantId,
                        ScheduleDate = bookingDetail.RestaurantSchedule.ScheduleDate,
                        Id = bookingDetail.RestaurantSchedule.Id,
                        StartTime = bookingDetail.RestaurantSchedule.StartTime,
                        Status = bookingDetail.RestaurantSchedule.Status
                    },
                    ScheduleID = bookingDetail.RestaurantScheduleId,
                };
                bookingHistoryDto.BookingDetails.Add(bookingDetailDto);
            }
            logger.Info("bookinghistory created : {@req} ", bookingHistoryDto);

            return bookingHistoryDto;
        }
    }
}