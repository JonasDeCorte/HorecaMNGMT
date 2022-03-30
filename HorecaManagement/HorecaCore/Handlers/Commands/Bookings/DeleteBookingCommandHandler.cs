using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Bookings
{
    public class DeleteBookingCommand : IRequest<int>
    {
        public DeleteBookingCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteBookingCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(Booking), request.Id);

            repository.Bookings.Delete(request.Id);

            await repository.CommitAsync();

            logger.Info("deleted {object} with Id: {id}", nameof(Booking), request.Id);

            return request.Id;
        }
    }
}