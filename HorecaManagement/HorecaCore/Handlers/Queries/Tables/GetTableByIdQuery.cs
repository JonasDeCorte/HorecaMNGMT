using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Tables;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Tables
{
    public class GetTableByIdQuery : IRequest<TableDto>
    {
        public int TableId { get; }

        public GetTableByIdQuery(int tableId)
        {
            TableId = tableId;
        }

        public class GetDishByIdQueryHandler : IRequestHandler<GetTableByIdQuery, TableDto>
        {
            private readonly IUnitOfWork repository;
            private readonly IMapper mapper;

            public GetDishByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<TableDto> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
            {
                var table = await Task.FromResult(repository.Tables.GetTableIncludingDependencies(request.TableId));

                if (table is null)
                {
                    throw new EntityNotFoundException($"No {nameof(table)} found for Id {request.TableId}");
                }

                return mapper.Map<TableDto>(table);
            }
        }
    }
}