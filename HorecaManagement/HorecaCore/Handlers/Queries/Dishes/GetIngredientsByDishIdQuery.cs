using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Dishes
{
    public class GetIngredientsByDishIdQuery : IRequest<DishIngredientsByIdDto>
    {
        public GetIngredientsByDishIdQuery(int dishId)
        {
            DishId = dishId;
        }

        public int DishId { get; }
    }

    public class GetIngredientsByDishIdHandler : IRequestHandler<GetIngredientsByDishIdQuery, DishIngredientsByIdDto>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper _mapper;

        public GetIngredientsByDishIdHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<DishIngredientsByIdDto> Handle(GetIngredientsByDishIdQuery request, CancellationToken cancellationToken)
        {
            var dish = await Task.FromResult(repository.Dishes.GetDishIncludingDependencies(request.DishId));
            if (dish is null)
            {
                throw new EntityNotFoundException($"No Dish found for Id {request.DishId}");
            }

            return _mapper.Map<DishIngredientsByIdDto>(dish);
        }
    }
}