using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Units;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Units
{
    public class GetAllUnitsQuery : IRequest<IEnumerable<UnitDto>>
    {
    }

    public class GetAllIngrdedientsQueryHandler : IRequestHandler<GetAllUnitsQuery, IEnumerable<UnitDto>>

    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllIngrdedientsQueryHandler(IUnitOfWork repository, IMapper mapper)

        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UnitDto>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)

        {
            var entities = await Task.FromResult(_repository.Units.GetAll());
            return _mapper.Map<IEnumerable<UnitDto>>(entities);
        }
    }
}