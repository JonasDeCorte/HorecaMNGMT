using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Menus;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Menus
{
    public class GetAllMenusQuery : IRequest<IEnumerable<MenuDto>>
    {
    }

    public class GetAllMenuQueryHandler : IRequestHandler<GetAllMenusQuery, IEnumerable<MenuDto>>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper _mapper;

        public GetAllMenuQueryHandler(IUnitOfWork repository, IMapper mapper)

        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuDto>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)

        {
            var entities = await Task.FromResult(repository.Menus.GetAll());
            return _mapper.Map<IEnumerable<MenuDto>>(entities);
        }
    }
}