using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Permissions
{
    public class GetPermissionByIdQuery : IRequest<PermissionDto>
    {
        public GetPermissionByIdQuery(int permissionId)
        {
            PermissionId = permissionId;
        }

        public int PermissionId { get; }
    }

    public class GetPermissionByIdQueryHandler : IRequestHandler<GetPermissionByIdQuery, PermissionDto>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetPermissionByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PermissionDto> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to return {object} with id: {id}", nameof(PermissionDto), request.PermissionId);

            var permission = await Task.FromResult(repository.Menus.Get(request.PermissionId));

            if (permission is null)
            {
                logger.Error("{object} with Id: {id} is null", nameof(permission), request.PermissionId);

                throw new EntityNotFoundException("role does not exist");
            }
            logger.Info("returning {@object} with id: {id}", permission, request.PermissionId);

            return mapper.Map<PermissionDto>(permission);
        }
    }
}