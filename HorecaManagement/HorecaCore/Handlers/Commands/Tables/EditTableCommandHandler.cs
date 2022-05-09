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

            table.Seats = request.Model.Seats ?? table.Seats;
            table.Name = request.Model.Name ?? table.Name;
            table.Src = request.Model.Src ?? table.Src;
            table.Type = request.Model.Type ?? table.Type;
            table.OriginX = request.Model.OriginX ?? table.OriginX;
            table.OriginY = request.Model.OriginY ?? table.OriginY;
            if (request.Model.Pax != table.Pax)
                table.Pax = request.Model.Pax;
            if (request.Model.Left != table.Left)
                table.Left = request.Model.Left;
            if (request.Model.Top != table.Top)
                table.Top = request.Model.Top;
            if (request.Model.Width != table.Width)
                table.Width = request.Model.Width;
            if (request.Model.Height != table.Height)
                table.Height = request.Model.Height;
            if (request.Model.ScaleX != table.ScaleX)
                table.ScaleX = request.Model.ScaleX;
            if (request.Model.ScaleY != table.ScaleY)
                table.ScaleY = request.Model.ScaleY;

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
