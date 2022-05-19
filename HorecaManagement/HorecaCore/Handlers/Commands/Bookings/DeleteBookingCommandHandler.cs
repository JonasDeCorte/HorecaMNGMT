using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;
using NLog;
using static Horeca.Shared.Utils.Constants;

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
            var booking = await repository.Bookings.GetBookingById(request.Id);
            if (booking == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            booking.Schedule.AvailableSeat += booking.Pax;
            CheckScheduleStatus(booking);
            repository.Schedules.Update(booking.Schedule);
            repository.Bookings.Delete(booking.Id);

            await repository.CommitAsync();

            logger.Info("deleted {object} with Id: {id}", nameof(Booking), request.Id);

            return request.Id;
        }

        private static void CheckScheduleStatus(Booking booking)
        {
            if (booking.Schedule.AvailableSeat > 0 && booking.Schedule.Status.Equals(ScheduleStatus.Full))
            {
                booking.Schedule.Status = ScheduleStatus.Available;
            }
        }
    }
}