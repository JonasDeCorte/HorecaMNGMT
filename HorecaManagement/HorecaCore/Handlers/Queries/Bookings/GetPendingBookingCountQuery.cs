using Horeca.Shared.Data;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Bookings
{
    public class GetPendingBookingCountQuery : IRequest<int>
    {
        public GetPendingBookingCountQuery()
        {
        }
    }

    public class GetPendingBookingCountHandler : IRequestHandler<GetPendingBookingCountQuery, int>
    {
        private readonly IUnitOfWork repository;

        public GetPendingBookingCountHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(GetPendingBookingCountQuery request, CancellationToken cancellationToken)
        {
            return repository.BookingRepository.AdminGetPendingBookingCount();
        }
    }
}