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

        public GetMenuByIdQuery(int menuId)
        {
            MenuId = menuId;
        }

        public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, MenuDto>
        {
            private readonly IUnitOfWork repository;
            private readonly IMapper _mapper;
            private static Logger logger = LogManager.GetCurrentClassLogger();

            public GetMenuByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                this.repository = repository;
                _mapper = mapper;
            }

            public async Task<MenuDto> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
            {
                logger.Info("trying to return {object} with id: {id}", nameof(MenuDto), request.MenuId);

                var menu = await Task.FromResult(repository.Menus.Get(request.MenuId));

                if (menu is null)
                {
                    logger.Error("{object} with Id: {id} is null", nameof(menu), request.MenuId);

                    throw new EntityNotFoundException($"No menu found for Id {request.MenuId}");
                }
                logger.Info("returning {@object} with id: {id}", menu, request.MenuId);

                return _mapper.Map<MenuDto>(menu);
            }
        }
    }
}