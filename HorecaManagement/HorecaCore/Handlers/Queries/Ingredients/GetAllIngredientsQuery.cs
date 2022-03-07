using AutoMapper;
using Horeca.Core.Constants;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Ingredients
{
    /// <summary>
    ///  We begin by creating a "QueryRequest" that represents this request in the Mediator layer.
    /// "The IRequest interface accepts the type which the Handler should return to the calling component.
    /// The Handler must return an IEnumerable to the calling client which is our controller.
    /// </summary>
    public class GetAllIngredientsQuery : IRequest<IEnumerable<IngredientDto>>
    {
    }

    /// <summary>
    /// We next add a Handler which receives this request via the Mediator and processes it.
    /// "The IRequestHandler accepts two type parameters: the Request to which is must respond and the type it must return."
    /// </summary>
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
            var entities = await Task.FromResult(_repository.Ingredients.GetAllIncludingUnit());
            return _mapper.Map<IEnumerable<IngredientDto>>(entities);
        }
    }
}