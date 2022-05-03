using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Floorplans;
using Horeca.Shared.Dtos.Restaurants;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Floorplans
{
    public class GetAllFloorplansQuery : IRequest<IEnumerable<FloorplanDto>>
    {
        public GetAllFloorplansQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public int RestaurantId { get; }
    }

    public class GetAllFloorplansQueryHandler : IRequestHandler<GetAllFloorplansQuery, IEnumerable<FloorplanDto>>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllFloorplansQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<FloorplanDto>> Handle(GetAllFloorplansQuery request, CancellationToken cancellationToken)
        {
            var entities = await repository.Floorplans.GetAllFloorplans(request.RestaurantId);

            logger.Info("{amount} of {nameof} have been returned", entities.Count(), nameof(FloorplanDto));

            var floorplanDtos = new List<FloorplanDto>();
            foreach (var entity in entities)
            {
                FloorplanDto dto = new FloorplanDto
                {
                    Id = entity.Id,
                    Restaurant = new RestaurantDto()
                    {
                        Id = entity.Restaurant.Id,
                        Name = entity.Restaurant.Name
                    }
                };
                floorplanDtos.Add(dto);
            }
            return floorplanDtos;

            //return mapper.Map<IEnumerable<FloorplanDto>>(entities);
        }
    }
}
