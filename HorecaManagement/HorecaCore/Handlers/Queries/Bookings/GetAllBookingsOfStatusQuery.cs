using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Bookings;
using Horeca.Shared.Utils;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Bookings
{
    public class GetAllBookingsOfStatusQuery : IRequest<IEnumerable<BookingDto>>
    {
        public GetAllBookingsOfStatusQuery(string status, int scheduleId)
        {
            Status = status;
            ScheduleId = scheduleId;
        }

        public string Status { get; }
        public int ScheduleId { get; }
    }

    public class GetAllBookingsOfStatusQueryHandler : IRequestHandler<GetAllBookingsOfStatusQuery, IEnumerable<BookingDto>>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllBookingsOfStatusQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookingDto>> Handle(GetAllBookingsOfStatusQuery request, CancellationToken cancellationToken)
        {
            logger.Info("requested to return bookings with request: {@req}", request);

            var bookings = await repository.Bookings.GetAllBookings(request.ScheduleId);

            logger.Info("bookings found with: {req} items", bookings.Count());

            bookings = FilterBookingStatus(bookings, request.Status);

            logger.Info("filtered bookings with status: {status} have been found: {req} items", request.Status, bookings.Count());

            return mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        private static IEnumerable<Booking> FilterBookingStatus(IEnumerable<Booking> bookings, string status)
        {
            status = char.ToUpper(status[0]) + status[1..];
            switch (status)
            {
                case Constants.BookingStatus.PENDING:
                    bookings = bookings.Where(b => b.BookingStatus.Equals(Constants.BookingStatus.PENDING));
                    break;

                case Constants.BookingStatus.EXPIRED:
                    bookings = bookings.Where(b => b.BookingStatus.Equals(Constants.BookingStatus.EXPIRED));
                    break;

                case Constants.BookingStatus.COMPLETE:
                    bookings = bookings.Where(b => b.BookingStatus.Equals(Constants.BookingStatus.COMPLETE));
                    break;
            }
            return bookings;
        }
    }
}