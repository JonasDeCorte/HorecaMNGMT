﻿using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Ingredients;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Ingredients
{
    public class GetIngredientByIdQuery : IRequest<IngredientDto>
    {
        public int IngredientId { get; }

        public GetIngredientByIdQuery(int ingredientId)
        {
            IngredientId = ingredientId;
        }

        public class GetIngredientByIdQueryHandler : IRequestHandler<GetIngredientByIdQuery, IngredientDto>
        {
            private readonly IUnitOfWork repository;
            private readonly IMapper _mapper;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public GetIngredientByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                this.repository = repository;
                _mapper = mapper;
            }

            public async Task<IngredientDto> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
            {
                logger.Info("trying to return {object} with id: {id}", nameof(IngredientDto), request.IngredientId);

                var ingredient = await Task.FromResult(repository.Ingredients.GetIngredientIncludingUnit(request.IngredientId));

                if (ingredient == null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }
                logger.Info("returning {@object} with id: {id}", ingredient, request.IngredientId);

                return _mapper.Map<IngredientDto>(ingredient);
            }
        }
    }
}