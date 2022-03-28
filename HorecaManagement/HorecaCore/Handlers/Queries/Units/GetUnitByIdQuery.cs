﻿using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Units;
using MediatR;
using NLog;

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
            private readonly IUnitOfWork repository;
            private readonly IMapper _mapper;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public GetUnitByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                this.repository = repository;
                _mapper = mapper;
            }

            public async Task<UnitDto> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
            {
                var unit = await Task.FromResult(repository.Units.Get(request.UnitId));

                if (unit == null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }
                logger.Info("returning {@object} with id: {id}", unit, request.UnitId);
                return _mapper.Map<UnitDto>(unit);
            }
        }
    }
}