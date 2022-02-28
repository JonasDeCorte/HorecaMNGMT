using AutoMapper;
using HorecaShared.Data;
using HorecaShared.Dtos;
using MediatR;

namespace Horeca.Core.Providers.Handlers.Queries
{
    public class GetAllIngredientsQuery : IRequest<IEnumerable<IngredientDto>>
    {
    }

    public class GetAllIngrdedientsQueryHandler : IRequestHandler<GetAllIngredientsQuery, IEnumerable<IngredientDto>>

    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllIngrdedientsQueryHandler(IUnitOfWork repository, IMapper mapper)

        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IngredientDto>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)

        {
            var entities = await Task.FromResult(_repository.Ingredients.GetAll());
            return _mapper.Map<IEnumerable<IngredientDto>>(entities);
        }
    }
}