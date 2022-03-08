using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;

namespace Horeca.Core.Handlers.Queries.MenuCards
{
    public class GetMenuCardByIdQuery : IRequest<MenuCardDto>
    {
        public GetMenuCardByIdQuery(int menuCardId)
        {
            MenuCardId = menuCardId;
        }

        public int MenuCardId { get; }
    }

    public class GetMenuCardByIdQueryHandler : IRequestHandler<GetMenuCardByIdQuery, MenuCardDto>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;

        public GetMenuCardByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<MenuCardDto> Handle(GetMenuCardByIdQuery request, CancellationToken cancellationToken)
        {
            var menuCard = await Task.FromResult(repository.MenuCards.Get(request.MenuCardId));

            if (menuCard is null)
            {
                throw new EntityNotFoundException($"No MenuCard found for Id {request.MenuCardId}");
            }

            return mapper.Map<MenuCardDto>(menuCard);
        }
    }
}