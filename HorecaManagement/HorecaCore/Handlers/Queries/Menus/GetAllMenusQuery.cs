using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Menus
{
    public class GetAllMenusQuery : IRequest<IEnumerable<MenuDto>>
    {
    }

    public class GetAllMenuQueryHandler : IRequestHandler<GetAllMenusQuery, IEnumerable<MenuDto>>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper _mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllMenuQueryHandler(IUnitOfWork repository, IMapper mapper)

        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuDto>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)

        {
            var entities = await Task.FromResult(repository.Menus.GetAll());

            logger.Info("{amount} of {nameof} have been returned", entities.Count(), nameof(MenuDto));

            return _mapper.Map<IEnumerable<MenuDto>>(entities);
        }
    }
}