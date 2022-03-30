using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Bookings;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Bookings
{
    public class GetByBookingNoQuery : IRequest<BookingDto>
    {
        public GetByBookingNoQuery(string bookingNo)
        {
            BookingNo = bookingNo;
        }

        public string BookingNo { get; }
    }

    public class GetByBookingNoQueryHandler : IRequestHandler<GetByBookingNoQuery, BookingDto>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetByBookingNoQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BookingDto> Handle(GetByBookingNoQuery request, CancellationToken cancellationToken)
        {
            logger.Info("requested to return bookingDto with request: {@req}", request);

            Booking? booking = await repository.Bookings.GetByNumber(request.BookingNo);

            logger.Info("returning booking : {req} ", booking.Id);

            return mapper.Map<BookingDto>(booking);
        }
    }
}