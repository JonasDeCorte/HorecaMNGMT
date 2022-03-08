using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Dishes
{
    public class GetAllDishesQuery : IRequest<IEnumerable<DishDto>>
    {
    }

    public class GetAllDishesQueryHandler : IRequestHandler<GetAllDishesQuery, IEnumerable<DishDto>>

    {
        private readonly IUnitOfWork repository;
        private readonly IMapper _mapper;

        public GetAllDishesQueryHandler(IUnitOfWork repository, IMapper mapper)

        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DishDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)

        {
            var entities = await Task.FromResult(repository.Dishes.GetAll());
            return _mapper.Map<IEnumerable<DishDto>>(entities);
        }
    }
}