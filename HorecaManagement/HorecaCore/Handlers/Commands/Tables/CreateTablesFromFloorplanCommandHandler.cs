using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Floorplans;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Tables
{
    public class CreateTablesFromFloorplanCommand : IRequest<IEnumerable<Table>>
    {
        public FloorplanDetailDto Model { get; }
        public int Id { get; }
        public int RestaurantId { get; }

        public CreateTablesFromFloorplanCommand(FloorplanDetailDto model, int id, int restaurantId)
        {
            Model = model;
            Id = id;
            RestaurantId = restaurantId;
        }
    }

    public class CreateTablesFromFloorplanCommandHandler : IRequestHandler<CreateTablesFromFloorplanCommand, IEnumerable<Table>>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public CreateTablesFromFloorplanCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Table>> Handle(CreateTablesFromFloorplanCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            logger.Info("trying to create {object}", nameof(IEnumerable<Table>));
            var restaurant = repository.Restaurants.Get(request.Model.Restaurant.Id);
            var floorplan = repository.Floorplans.Get(request.Model.Id);

            if (restaurant == null || floorplan == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            var tables = new List<Table>();

            foreach (var tableDto in request.Model.Tables)
            {
                var table = new Table()
                {
                    Id = tableDto.Id,
                    FloorplanId = tableDto.FloorplanId,
                    ScheduleId = tableDto.ScheduleId,
                    Pax = tableDto.Pax,
                    Seats = tableDto.Seats,
                    Name = tableDto.Name,
                    Src = tableDto.Src,
                    Type = tableDto.Type,
                    OriginX = tableDto.OriginX,
                    OriginY = tableDto.OriginY,
                    Left = tableDto.Left,
                    Top = tableDto.Top,
                    Width = tableDto.Width,
                    Height = tableDto.Height,
                    ScaleX = tableDto.ScaleX,
                    ScaleY = tableDto.ScaleY,
                };
                repository.Tables.Add(table);
                await repository.CommitAsync();
                logger.Info("adding {@object} with id {id}", table, table.Id);
                tables.Add(table);
            }

            return tables;
        }

        private static void ValidateModelIds(CreateTablesFromFloorplanCommand request)
        {
            if (request.Model.Restaurant.Id == 0)
            {
                request.Model.Restaurant.Id = request.RestaurantId;
            }
        }
    }
}
