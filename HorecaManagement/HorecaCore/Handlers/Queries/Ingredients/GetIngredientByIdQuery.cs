using AutoMapper;
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
            private static Logger logger = LogManager.GetCurrentClassLogger();

            public GetIngredientByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                this.repository = repository;
                _mapper = mapper;
            }

            public async Task<IngredientDto> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
            {
                var ingredient = await Task.FromResult(repository.Ingredients.GetIngredientIncludingUnit(request.IngredientId));

                if (ingredient == null)
                {
                    logger.Error("{object} with Id: {id} is null", nameof(ingredient), request.IngredientId);

                    throw new EntityNotFoundException($"No Ingredient found for Id {request.IngredientId}");
                }
                logger.Info("returning {@object} with id: {id}", ingredient, request.IngredientId);

                return _mapper.Map<IngredientDto>(ingredient);
            }
        }
    }
}