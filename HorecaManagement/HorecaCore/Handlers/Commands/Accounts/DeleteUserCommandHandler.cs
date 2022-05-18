using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Data.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class DeleteUserCommand : IRequest<string>
    {
        public string Username { get; set; }

        public DeleteUserCommand(string username)
        {
            Username = username;
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
    {
        private readonly IUnitOfWork repository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationDbContext context;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteUserCommandHandler(IUnitOfWork repository, UserManager<ApplicationUser> userManager, IApplicationDbContext context)
        {
            this.repository = repository;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(ApplicationUser), request.Username);

            var user = await userManager.FindByNameAsync(request.Username);
            var bookings = await repository.Bookings.GetDetailsByUserId(user.Id, "all");
            if (user == null || bookings == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            foreach (var booking in bookings)
            {
                var sched = repository.Schedules.Get(booking.ScheduleId);
                sched.AvailableSeat += booking.Pax;
                repository.Schedules.Update(sched);

                var tables = context.Tables.Where(x => x.BookingId.Equals(booking.Id));
                foreach (var table in tables)
                {
                    table.BookingId = null ;
                    repository.Tables.Update(table);
                }
                repository.Bookings.Delete(booking.Id);
            }
            await repository.CommitAsync();

            // Constraints Oplossen
            await userManager.DeleteAsync(user);

            logger.Info("deleted {object} with Id: {id}", nameof(ApplicationUser), request.Username);

            return request.Username;
        }
    }
}
