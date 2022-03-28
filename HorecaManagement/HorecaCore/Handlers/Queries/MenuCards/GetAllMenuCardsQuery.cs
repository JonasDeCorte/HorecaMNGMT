using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.MenuCards
{
    public class GetAllMenuCardsQuery : IRequest<IEnumerable<MenuCardDto>>
    {
        public GetAllMenuCardsQuery()
        {
        }
    }

    public class GetAllMenuCardsQueryHandler : IRequestHandler<GetAllMenuCardsQuery, IEnumerable<MenuCardDto>>

    {
        private readonly IUnitOfWork repository;
        private readonly IMapper _mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllMenuCardsQueryHandler(IUnitOfWork repository, IMapper mapper)

        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuCardDto>> Handle(GetAllMenuCardsQuery request, CancellationToken cancellationToken)

        {
            var entities = await Task.FromResult(repository.MenuCards.GetAll());
            logger.Info("{amount} of {nameof} have been returned", entities.Count(), nameof(MenuCardDto));

            return _mapper.Map<IEnumerable<MenuCardDto>>(entities);
        }
    }
}