using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
using MediatR;
using NLog;

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
            private readonly IMapper mapper;
            private static Logger logger = LogManager.GetCurrentClassLogger();

            public GetDishByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
            {
                logger.Info("trying to return {object} with id: {id}", nameof(DishDto), request.DishId);

                var dish = await Task.FromResult(repository.Dishes.Get(request.DishId));

                if (dish is null)
                {
                    logger.Error("{object} with Id: {id} is null", nameof(dish), request.DishId);

                    throw new EntityNotFoundException($"No Dish found for Id {request.DishId}");
                }
                logger.Info("returning {@object} with id: {id}", dish, request.DishId);

                return mapper.Map<DishDto>(dish);
            }
        }
    }
}