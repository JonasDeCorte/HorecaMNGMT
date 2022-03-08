using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Units;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Units
{
    public class EditUnitCommand : IRequest<int>
    {
        public MutateUnitDto Model { get; }

        public EditUnitCommand(MutateUnitDto model)
        {
            Model = model;
        }
    }

    public class EditUnitCommandHandler : IRequestHandler<EditUnitCommand, int>
    {
        private readonly IUnitOfWork repository;

        public EditUnitCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = repository.Units.Get(request.Model.Id);

            if (unit is null)
            {
                throw new EntityNotFoundException("Entity does not exist");
            }

            unit.Name = request.Model.Name ?? unit.Name;

            repository.Units.Update(unit);

            await repository.CommitAsync();

            return request.Model.Id;
        }
    }
}