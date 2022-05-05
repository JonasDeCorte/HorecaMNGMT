using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Floorplans;
using Horeca.Shared.Dtos.Restaurants;
using Horeca.Shared.Dtos.Tables;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Floorplans
{
    public class GetFloorplanByIdQuery : IRequest<FloorplanDetailDto>
    {
        public int FloorplanId { get; }
        public int RestaurantId { get; }

        public GetFloorplanByIdQuery(int floorplanId, int restaurantId)
        {
            FloorplanId = floorplanId;
            RestaurantId = restaurantId;
        }

        public class GetFloorplanByIdQueryHandler : IRequestHandler<GetFloorplanByIdQuery, FloorplanDetailDto>
        {
            private readonly IUnitOfWork repository;
            private readonly IMapper mapper;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public GetFloorplanByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<FloorplanDetailDto> Handle(GetFloorplanByIdQuery request, CancellationToken cancellationToken)
            {
                logger.Info("trying to return {object} with id: {id}", nameof(FloorplanDetailDto), request.FloorplanId);

                var floorplan = await repository.Floorplans.GetFloorplanById(request.FloorplanId, request.RestaurantId);
                var tables = await repository.Tables.GetAllTablesbyFloorplanId(request.FloorplanId);

                if (floorplan is null || tables is null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }
                logger.Info("returning {@object} with id: {id}", floorplan, request.FloorplanId);

                var floorplanDto = new FloorplanDetailDto()
                {
                    Id = floorplan.Id,
                    Name = floorplan.Name,
                    Tables = mapper.Map<List<MutateTableDto>>(tables),
                    Restaurant = new RestaurantDto()
                    {
                        Id = floorplan.Restaurant.Id,
                        Name = floorplan.Restaurant.Name,
                    }
                };

                return floorplanDto;
            }
        }
    }
}
