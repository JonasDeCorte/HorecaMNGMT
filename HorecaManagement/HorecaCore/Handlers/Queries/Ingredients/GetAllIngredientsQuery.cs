﻿using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Ingredients;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Ingredients
{
    public class GetAllIngredientsQuery : IRequest<IEnumerable<IngredientDto>>
    {
    }

    public class GetAllIngrdedientsQueryHandler : IRequestHandler<GetAllIngredientsQuery, IEnumerable<IngredientDto>>

    {
        private readonly IUnitOfWork repository;
        private readonly IMapper _mapper;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllIngrdedientsQueryHandler(IUnitOfWork repository, IMapper mapper)

        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IngredientDto>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)

        {
            var entities = await Task.FromResult(repository.Ingredients.GetAllIncludingUnit());
            logger.Info("{amount} of {nameof} have been returned", entities.Count(), nameof(IngredientDto));

            return _mapper.Map<IEnumerable<IngredientDto>>(entities);
        }
    }
}