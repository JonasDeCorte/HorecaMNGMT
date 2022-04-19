using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Menus
{
    public class GetAllMenusQuery : IRequest<IEnumerable<MenuDto>>
    {
        public GetAllMenusQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public int RestaurantId { get; }
    }

    public class GetAllMenuQueryHandler : IRequestHandler<GetAllMenusQuery, IEnumerable<MenuDto>>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllMenuQueryHandler(IUnitOfWork repository, IMapper mapper)

        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<MenuDto>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)

        {
            var entities = await repository.Menus.GetAllMenus(request.RestaurantId);

            logger.Info("{amount} of {nameof} have been returned", entities.Count(), nameof(MenuDto));

            return mapper.Map<IEnumerable<MenuDto>>(entities);
        }
    }
}