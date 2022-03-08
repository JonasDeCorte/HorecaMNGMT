using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Menus;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Menus
{
    public class GetMenuByIdQuery : IRequest<MenuDto>
    {
        public int MenuId { get; }

        public GetMenuByIdQuery(int menuId)
        {
            MenuId = menuId;
        }

        public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, MenuDto>
        {
            private readonly IUnitOfWork _repository;
            private readonly IMapper _mapper;

            public GetMenuByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<MenuDto> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
            {
                var menu = await Task.FromResult(_repository.Menus.Get(request.MenuId));

                if (menu is null)
                {
                    throw new EntityNotFoundException($"No menu found for Id {request.MenuId}");
                }

                return _mapper.Map<MenuDto>(menu);
            }
        }
    }
}