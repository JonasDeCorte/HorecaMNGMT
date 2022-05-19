using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Bookings;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Bookings
{
    public class GetAllBookingsByUserIdQuery : IRequest<BookingHistoryDto>
    {
        public GetAllBookingsByUserIdQuery(string userId, string status)
        {
            UserId = userId;
            Status = status;
        }

        public string UserId { get; }
        public string Status { get; }
    }

    public class GetAllBookingsByUserIdQueryHandler : IRequestHandler<GetAllBookingsByUserIdQuery, BookingHistoryDto>
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IUnitOfWork repository;

        public GetAllBookingsByUserIdQueryHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<BookingHistoryDto> Handle(GetAllBookingsByUserIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("requested to return bookinghistory with request: {@req}", request);

            var bookings = await repository.Bookings.GetDetailsByUserId(request.UserId, request.Status);
            logger.Info("bookinglist found with: {req} items", bookings.Count());
            BookingHistoryDto dto = new();

            MapToBookingHistoryDto(bookings, dto);

            return dto;
        }

        private static void MapToBookingHistoryDto(IEnumerable<Booking> bookings, BookingHistoryDto dto)
        {
            foreach (var b in bookings)
            {
                dto.BookingDetails.Add(new BookingDto
                {
                    BookingDate = b.BookingDate,
                    BookingNo = b.BookingNo,
                    BookingStatus = b.BookingStatus,
                    CheckIn = b.CheckIn,
                    CheckOut = b.CheckOut,
                    FullName = b.FullName,
                    Id = b.Id,
                    PhoneNo = b.PhoneNo,
                    UserId = b.UserId,
                    Pax = b.Pax,
                    ScheduleId = b.ScheduleId,
                    RestaurantId = b.RestaurantId
                });
            }
        }
    }
}