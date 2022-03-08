using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Dishes
{
    public class GetDishByIdQuery : IRequest<DishDto>
    {
        public int DishId { get; }

        public GetDishByIdQuery(int dishId)
        {
            DishId = dishId;
        }

        public class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, DishDto>
        {
            private readonly IUnitOfWork repository;
            private readonly IMapper _mapper;

            public GetDishByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                this.repository = repository;
                _mapper = mapper;
            }

            public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
            {
                var dish = await Task.FromResult(repository.Dishes.Get(request.DishId));

                if (dish is null)
                {
                    throw new EntityNotFoundException($"No Dish found for Id {request.DishId}");
                }

                return _mapper.Map<DishDto>(dish);
            }
        }
    }
}