using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Bookings;
using MediatR;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Core.Handlers.Queries.Bookings
{
    public class GetAllBookingsByScheduleIdQuery : IRequest<IEnumerable<BookingDetailOnlyBookingsDto>>
    {
        public GetAllBookingsByScheduleIdQuery(int scheduleId)
        {
            ScheduleId = scheduleId;
        }

        public int ScheduleId { get; }
    }

    public class GetAllBookingsByScheduleIdQueryHandler : IRequestHandler<GetAllBookingsByScheduleIdQuery, IEnumerable<BookingDetailOnlyBookingsDto>>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllBookingsByScheduleIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookingDetailOnlyBookingsDto>> Handle(GetAllBookingsByScheduleIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("requested to return bookings with request: {@req}", request);

            IEnumerable<Shared.Data.Entities.BookingDetail>? bookingDetails = await repository.BookingDetails.GetDetailsForRestaurantSchedule(request.ScheduleId);

            logger.Info("bookings found with: {req} items", bookingDetails.Count());

            return mapper.Map<IEnumerable<BookingDetailOnlyBookingsDto>>(bookingDetails);
        }
    }
}