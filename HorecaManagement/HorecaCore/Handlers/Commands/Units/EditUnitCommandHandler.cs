using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Units;
using MediatR;
using NLog;

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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EditUnitCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditUnitCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to edit {object} with Id: {Id}", nameof(Shared.Data.Entities.Unit), request.Model.Id);

            var unit = await repository.Units.GetUnitById(request.Model.Id, request.Model.RestaurantId);

            if (unit is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            unit.Name = request.Model.Name ?? unit.Name;

            repository.Units.Update(unit);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", unit, unit.Id);

            return unit.Id;
        }
    }
}