using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Tables;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Tables
{
    public class EditTableCommand : IRequest<int>
    {
        public EditTableDto Model { get; }

        public EditTableCommand(EditTableDto model)
        {
            Model = model;
        }
    }

    public class EditTableCommandHandler : IRequestHandler<EditTableCommand, int>
    {
        private readonly IUnitOfWork repository;

        public EditTableCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditTableCommand request, CancellationToken cancellationToken)
        {
            var table = repository.Tables.GetTableIncludingDependencies(request.Model.Id);

            if (table is null)
            {
                throw new EntityNotFoundException("Table does not exist");
            }

            table.Name = request.Model.Name ?? table.Name;

            if (table.AmountOfPeople != request.Model.AmountOfPeople)
            {
                table.AmountOfPeople = request.Model.AmountOfPeople;
            }

            if (table.Reservation is not null)
            {
                Reservation reservation = repository.Reservations.GetReservationIncludingDependencies(request.Model.Reservation.Id);
                if (!table.Reservation.Equals(reservation))
                {
                    table.Reservation = reservation;
                    repository.Reservations.Update(reservation);
                }
            }

            table.HasReservation = table.Reservation is not null;

            repository.Tables.Update(table);

            await repository.CommitAsync();

            return table.Id;
        }
    }
}