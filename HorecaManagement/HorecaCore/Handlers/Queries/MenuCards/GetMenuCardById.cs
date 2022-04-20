using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.MenuCards
{
    public class GetMenuCardByIdQuery : IRequest<MenuCardDto>
    {
        public GetMenuCardByIdQuery(int menuCardId, int restaurantId)
        {
            MenuCardId = menuCardId;
            RestaurantId = restaurantId;
        }

        public int MenuCardId { get; }
        public int RestaurantId { get; }
    }

    public class GetMenuCardByIdQueryHandler : IRequestHandler<GetMenuCardByIdQuery, MenuCardDto>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetMenuCardByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<MenuCardDto> Handle(GetMenuCardByIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to return {object} with id: {id}", nameof(MenuCardDto), request.MenuCardId);

            var menuCard = await repository.MenuCards.GetMenuCardById(request.MenuCardId, request.RestaurantId);

            if (menuCard is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            logger.Info("returning {@object} with id: {id}", menuCard, request.MenuCardId);

            return mapper.Map<MenuCardDto>(menuCard);
        }
    }
}