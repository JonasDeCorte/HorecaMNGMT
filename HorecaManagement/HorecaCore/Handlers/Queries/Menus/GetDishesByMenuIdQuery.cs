using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Core.Handlers.Queries.Menus
{
    public class GetDishesByMenuIdQuery : IRequest<MenuDishesByIdDto>
    {
        public GetDishesByMenuIdQuery(int menuId)
        {
            MenuId = menuId;
        }

        public int MenuId { get; }
    }

    public class GetDishesByMenuIdHandler : IRequestHandler<GetDishesByMenuIdQuery, MenuDishesByIdDto>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;

        public GetDishesByMenuIdHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<MenuDishesByIdDto> Handle(GetDishesByMenuIdQuery request, CancellationToken cancellationToken)
        {
            var menu = await Task.FromResult(repository.Menus.GetMenuIncludingDependencies(request.MenuId));
            if (menu is null)
            {
                throw new EntityNotFoundException($"No Menu found for Id {request.MenuId}");
            }
            return mapper.Map<MenuDishesByIdDto>(menu);
        }
    }
}