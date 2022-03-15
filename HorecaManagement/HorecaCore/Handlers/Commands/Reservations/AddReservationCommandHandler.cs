using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Reservation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Core.Handlers.Commands.Reservations
{
    public class AddReservationCommand : IRequest<int>
    {
        public AddReservationCommand(MutateReservationDto model)
        {
            Model = model;
        }

        public MutateReservationDto Model { get; }
    }

    public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, int>
    {
        private readonly IUnitOfWork repository;
        private readonly IValidator<MutateReservationDto> validator;

        public AddReservationCommandHandler(IUnitOfWork repository, IValidator<MutateReservationDto> validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public async Task<int> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {
            var result = validator.Validate(request.Model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }
            Table table = repository.Tables.Get(request.Model.TableId);
            if (table is not null)
                throw new EntityNotFoundException($"{nameof(table)} with Id: {request.Model.Id}. Not found.");

            var entity = new Reservation
            {
                AmountOfPeople = request.Model.AmountOfPeople,
                DateOfReservation = request.Model.DateOfReservation,
                Email = request.Model.Email,
                Name = request.Model.Name,
                Table = table,
                TimeOfReservation = request.Model.TimeOfReservation,
            };
            repository.Reservations.Add(entity);
            await repository.CommitAsync();

            return entity.Id;
        }
    }
}