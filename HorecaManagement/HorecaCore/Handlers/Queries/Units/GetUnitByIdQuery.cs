using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Units;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Units
{
    public class GetUnitByIdQuery : IRequest<UnitDto>
    {
        public int UnitId { get; }

        public GetUnitByIdQuery(int unitId)
        {
            UnitId = unitId;
        }

        public class GetUnitByIdQueryHandler : IRequestHandler<GetUnitByIdQuery, UnitDto>
        {
            private readonly IUnitOfWork _repository;
            private readonly IMapper _mapper;

            public GetUnitByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<UnitDto> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
            {
                var unit = await Task.FromResult(_repository.Units.Get(request.UnitId));

                if (unit == null)
                {
                    throw new EntityNotFoundException($"No Unit found for Id {request.UnitId}");
                }

                return _mapper.Map<UnitDto>(unit);
            }
        }
    }
}