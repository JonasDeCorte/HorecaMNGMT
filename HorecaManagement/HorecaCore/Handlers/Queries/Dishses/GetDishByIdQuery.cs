using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Dishes
{
    public class GetDishByIdQuery : IRequest<DishDtoDetail>
    {
        public int DishId { get; }

        public GetDishByIdQuery(int dishId)
        {
            DishId = dishId;
        }

        public class GetIngredientByIdQueryHandler : IRequestHandler<GetDishByIdQuery, DishDtoDetail>
        {
            private readonly IUnitOfWork _repository;
            private readonly IMapper _mapper;

            public GetIngredientByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<DishDtoDetail> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
            {
                var dish = await Task.FromResult(_repository.Dishes.GetIncludingDependencies(request.DishId));

                if (dish is null)
                {
                    throw new EntityNotFoundException($"No Dish found for Id {request.DishId}");
                }

                return _mapper.Map<DishDtoDetail>(dish);
            }
        }
    }
}