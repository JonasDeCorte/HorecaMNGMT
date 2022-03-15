using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Tables;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Tables
{
    public class GetAllTablesQuery : IRequest<IEnumerable<TableDto>>
    {
    }

    public class GetAllTablesQueryHandler : IRequestHandler<GetAllTablesQuery, IEnumerable<TableDto>>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;

        public GetAllTablesQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TableDto>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(repository.Tables.GetTablesIncludingDependencies());

            return mapper.Map<IEnumerable<TableDto>>(entities);
        }
    }
}