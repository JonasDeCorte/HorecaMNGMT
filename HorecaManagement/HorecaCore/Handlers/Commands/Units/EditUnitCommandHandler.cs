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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public EditUnitCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditUnitCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to edit {object} with Id: {Id}", nameof(Shared.Data.Entities.Unit), request.Model.Id);

            var unit = repository.Units.Get(request.Model.Id);

            if (unit is null)
            {
                logger.Error("{Object} with Id: {id} does not exist", nameof(Shared.Data.Entities.Unit), request.Model.Id);

                throw new EntityNotFoundException("Entity does not exist");
            }

            unit.Name = request.Model.Name ?? unit.Name;

            repository.Units.Update(unit);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", unit, unit.Id);

            return unit.Id;
        }
    }
}