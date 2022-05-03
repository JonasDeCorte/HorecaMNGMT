using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Tables;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Tables
{
    public class GetAllTablesQuery : IRequest<IEnumerable<TableDto>>
    {
        public GetAllTablesQuery(int floorplanId)
        {
            FloorplanId = floorplanId;
        }

        public int FloorplanId { get; }
    }

    public class GetAllTablesQueryHandler : IRequestHandler<GetAllTablesQuery, IEnumerable<TableDto>>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllTablesQueryHandler(IUnitOfWork repository, IMapper mapper)

        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TableDto>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            var entities = await repository.Tables.GetAllTablesbyFloorplanId(request.FloorplanId);

            logger.Info("{amount} of {nameof} have been returned", entities.Count(), nameof(TableDto));

            return mapper.Map<IEnumerable<TableDto>>(entities);
        }
    }
}
