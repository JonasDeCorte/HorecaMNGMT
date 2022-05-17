using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Tables;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Tables
{
    public class GetTableByIdQuery : IRequest<TableDto>
    {
        public int TableId { get; }
        public int FloorplanId { get; }

        public GetTableByIdQuery(int tableId, int floorplanId)
        {
            TableId = tableId;
            FloorplanId = floorplanId;
        }

        public class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, TableDto>
        {
            private readonly IUnitOfWork repository;
            private readonly IMapper mapper;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public GetTableByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<TableDto> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
            {
                logger.Info("trying to return {object} with id: {id}", nameof(TableDto), request.TableId);

                var table = await repository.Tables.GetTableById(request.TableId, request.FloorplanId);

                if (table is null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }
                logger.Info("returning {@object} with id: {id}", table, request.TableId);

                return mapper.Map<TableDto>(table);
            }
        }
    }
}
