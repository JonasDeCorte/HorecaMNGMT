using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;
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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetDishesByMenuIdHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<MenuDishesByIdDto> Handle(GetDishesByMenuIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to return {object} with id: {id}", nameof(MenuDishesByIdDto), request.MenuId);

            var menu = await Task.FromResult(repository.Menus.GetMenuIncludingDependencies(request.MenuId));
            if (menu is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            logger.Info("returning {@object} with id: {id}", menu, request.MenuId);

            return mapper.Map<MenuDishesByIdDto>(menu);
        }
    }
}