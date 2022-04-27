using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Menus
{
    public class GetDishesByMenuIdQuery : IRequest<MenuDishesByIdDto>
    {
        public GetDishesByMenuIdQuery(int menuId, int restaurantId)
        {
            MenuId = menuId;
            RestaurantId = restaurantId;
        }

        public int MenuId { get; }
        public int RestaurantId { get; }
    }

    public class GetDishesByMenuIdHandler : IRequestHandler<GetDishesByMenuIdQuery, MenuDishesByIdDto>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetDishesByMenuIdHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<MenuDishesByIdDto> Handle(GetDishesByMenuIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to return {object} with id: {id}", nameof(MenuDishesByIdDto), request.MenuId);

            var menu = await repository.Menus.GetMenuIncludingDependencies(request.MenuId, request.RestaurantId);
            if (menu is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            List<DishDto> dishDto = new();
            foreach (var item in menu.MenuDishes)
            {
                dishDto.Add(new DishDto
                {
                    Id = item.Dish.Id,
                    Category = item.Dish.Category,
                    Description = item.Dish.Description,
                    DishType = item.Dish.DishType,
                    Name = item.Dish.Name,
                    Price = item.Dish.Price,
                });
            }
            logger.Info("returning {object} wisth id: {id}", menu, request.MenuId);
            return new MenuDishesByIdDto()
            {
                Id = menu.Id,
                Dishes = dishDto
            };
        }
    }
}