using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Menus
{
    public class GetMenuByIdQuery : IRequest<MenuDto>
    {
        public int MenuId { get; }
        public int RestaurantId { get; }

        public GetMenuByIdQuery(int menuId, int restaurantId)
        {
            MenuId = menuId;
            RestaurantId = restaurantId;
        }

        public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, MenuDto>
        {
            private readonly IUnitOfWork repository;
            private readonly IMapper mapper;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public GetMenuByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<MenuDto> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
            {
                logger.Info("trying to return {object} with id: {id}", nameof(MenuDto), request.MenuId);

                var menu = await repository.Menus.GetMenuById(request.MenuId, request.RestaurantId);

                if (menu is null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }
                logger.Info("returning {@object} with id: {id}", menu, request.MenuId);

                return mapper.Map<MenuDto>(menu);
            }
        }
    }
}