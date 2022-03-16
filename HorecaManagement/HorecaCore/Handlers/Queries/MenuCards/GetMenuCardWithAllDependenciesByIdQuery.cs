using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.MenuCards
{
    public class GetMenuCardWithAllDependenciesByIdQuery : IRequest<MenuCardsByIdDto>
    {
        public GetMenuCardWithAllDependenciesByIdQuery(int menuCardId)
        {
            MenuCardId = menuCardId;
        }

        public int MenuCardId { get; }
    }

    public class GetMenuCardWithAllDependenciesByIdQueryHandler : IRequestHandler<GetMenuCardWithAllDependenciesByIdQuery, MenuCardsByIdDto>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public GetMenuCardWithAllDependenciesByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<MenuCardsByIdDto> Handle(GetMenuCardWithAllDependenciesByIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to return a full {object} with id: {id}", nameof(MenuCardsByIdDto), request.MenuCardId);

            var menuCard = await Task.FromResult(repository.MenuCards.GetMenuCardIncludingDependencies(request.MenuCardId));
            if (menuCard is null)
            {
                logger.Error("{object} with Id: {id} is null", nameof(menuCard), request.MenuCardId);

                throw new EntityNotFoundException($"No MenuCard found for Id {request.MenuCardId}");
            }

            logger.Info("returning {@object} with id: {id}", menuCard, request.MenuCardId);

            return mapper.Map<MenuCardsByIdDto>(menuCard);
        }
    }
}