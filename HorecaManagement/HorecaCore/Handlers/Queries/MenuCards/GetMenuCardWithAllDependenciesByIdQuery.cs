using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;

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

        public GetMenuCardWithAllDependenciesByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<MenuCardsByIdDto> Handle(GetMenuCardWithAllDependenciesByIdQuery request, CancellationToken cancellationToken)
        {
            var menuCard = await Task.FromResult(repository.MenuCards.GetMenuCardIncludingDependencies(request.MenuCardId));
            if (menuCard is null)
            {
                throw new EntityNotFoundException($"No MenuCard found for Id {request.MenuCardId}");
            }
            return mapper.Map<MenuCardsByIdDto>(menuCard);
        }
    }
}