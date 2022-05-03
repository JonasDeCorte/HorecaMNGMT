using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Floorplans;
using MediatR;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaCore.Handlers.Queries.Floorplans
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

                var floorplan = await repository.Dishes.GetDishById(request.FloorplanId, request.RestaurantId);

                if (floorplan is null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }
                logger.Info("returning {@object} with id: {id}", floorplan, request.FloorplanId);

                return mapper.Map<FloorplanDetailDto>(floorplan);
            }
        }
    }
}
