using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Permissions

{
    public class GetAllPermissionsQuery : IRequest<IEnumerable<PermissionDto>>
    {
        public GetAllPermissionsQuery()
        {
        }
    }

    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, IEnumerable<PermissionDto>>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllPermissionsQueryHandler(IUnitOfWork repository)

        {
            this.repository = repository;
        }

        public async Task<IEnumerable<PermissionDto>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var result = await Task.FromResult(repository.PermissionRepository.GetAll().Select(x => new PermissionDto
            {
                Id = x.Id,
                PermissionName = x.Name
            }).ToList());

            logger.Info("{amount} of {nameof} have been returned", result.Count, nameof(PermissionDto));

            return result;
        }
    }
}