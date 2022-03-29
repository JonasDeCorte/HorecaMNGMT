using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Bookings;
using Horeca.Shared.Utils;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Bookings
{
    public class GetAllBookingsOfStatusQuery : IRequest<IEnumerable<BookingDto>>
    {
        public GetAllBookingsOfStatusQuery(string status)
        {
            Status = status;
        }

        public string Status { get; }
    }

    public class GetAllBookingsOfStatusQueryHandler : IRequestHandler<GetAllBookingsOfStatusQuery, IEnumerable<BookingDto>>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;

        public GetAllBookingsOfStatusQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookingDto>> Handle(GetAllBookingsOfStatusQuery request, CancellationToken cancellationToken)
        {
            var bookings = repository.BookingRepository.GetAll();
            bookings = FilterBookingStatus(bookings, request.Status);

            return mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        private static IEnumerable<Booking> FilterBookingStatus(IEnumerable<Booking> bookings, string status)
        {
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