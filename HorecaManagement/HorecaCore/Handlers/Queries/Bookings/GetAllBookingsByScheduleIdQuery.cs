using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Bookings;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Bookings
{
    public class GetAllBookingsByScheduleIdQuery : IRequest<IEnumerable<BookingDto>>
    {
        public GetAllBookingsByScheduleIdQuery(int scheduleId)
        {
            ScheduleId = scheduleId;
        }

        public int ScheduleId { get; }
    }

    public class GetAllBookingsByScheduleIdQueryHandler : IRequestHandler<GetAllBookingsByScheduleIdQuery, IEnumerable<BookingDto>>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllBookingsByScheduleIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookingDto>> Handle(GetAllBookingsByScheduleIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("requested to return bookings with request: {@req}", request);

            var bookings = await repository.Bookings.GetBookingsForRestaurantSchedule(request.ScheduleId);

            logger.Info("bookings found with: {req} items", bookings.Count());

            var mapped = mapper.Map<IEnumerable<BookingDto>>(bookings);
            return mapped;
        }
    }
}