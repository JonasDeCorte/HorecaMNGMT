using Horeca.Shared.Data;
using MediatR;
using NLog;

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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetPendingBookingCountHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(GetPendingBookingCountQuery request, CancellationToken cancellationToken)
        {
            logger.Info("admin requested pending bookings created : {req} ", request);

            return repository.Bookings.AdminGetPendingBookingCount();
        }
    }
}