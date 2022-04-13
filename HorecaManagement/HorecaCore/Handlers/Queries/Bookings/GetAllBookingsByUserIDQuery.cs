using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
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
        private readonly IMapper mapper;

        public GetAllBookingsByUserIDQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BookingHistoryDto> Handle(GetAllBookingsByUserIDQuery request, CancellationToken cancellationToken)
        {
            logger.Info("requested to return bookinghistory with request: {@req}", request);

            var bookingDetailList = await repository.BookingDetails.GetDetailsByUserId(request.UserId, request.Status);
            logger.Info("bookinglist found with: {req} items", bookingDetailList.Count());
            BookingHistoryDto dto = new();

            MapToBookingHistoryDto(bookingDetailList, dto);

            return dto;
        }

        private static void MapToBookingHistoryDto(IEnumerable<BookingDetail> bookingDetailList, BookingHistoryDto dto)
        {
            foreach (var bd in bookingDetailList)
            {
                dto.BookingDetails.Add(new BookingDetailOnlyBookingsDto
                {
                    Booking = new BookingDto
                    {
                        BookingDate = bd.Booking.BookingDate,
                        BookingNo = bd.Booking.BookingNo,
                        BookingStatus = bd.Booking.BookingStatus,
                        CheckIn = bd.Booking.CheckIn,
                        CheckOut = bd.Booking.CheckOut,
                        FullName = bd.Booking.FullName,
                        Id = bd.Booking.Id,
                        PhoneNo = bd.Booking.PhoneNo,
                        UserID = bd.Booking.UserId
                    },
                    BookingId = bd.BookingId,
                    Pax = bd.Pax,
                });
            }
        }
    }
}