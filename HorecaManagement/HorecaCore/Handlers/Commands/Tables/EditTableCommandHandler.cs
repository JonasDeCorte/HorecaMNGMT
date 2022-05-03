using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Tables;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Tables
{
    public class EditTableCommand : IRequest<int>
    {
        public MutateTableDto Model { get; }
        public int Id { get; }
        public int FloorplanId { get; }

        public EditTableCommand(MutateTableDto model, int id, int floorplanId)
        {
            Model = model;
            Id = id;
            FloorplanId = floorplanId;
        }
    }

    public class EditDishCommandHandler : IRequestHandler<EditTableCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EditDishCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditTableCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            logger.Info("trying to edit {@object} with Id: {Id}", request.Model, request.Model.Id);

            var table = await repository.Tables.GetTableById(request.Model.Id, request.Model.FloorplanId);

            if (table is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            table.Name = request.Model.Name ?? table.Name;
            table.Pax = request.Model.Pax ?? table.Pax;
            table.Seats = request.Model.Seats ?? table.Seats;

            repository.Tables.Update(table);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", table, table.Id);

            return table.Id;
        }

        private static void ValidateModelIds(EditTableCommand request)
        {
            if (request.Model.Id == 0)
            {
                request.Model.Id = request.Id;
            }
            if (request.Model.FloorplanId == 0)
            {
                request.Model.FloorplanId = request.FloorplanId;
            }
        }
    }
}
