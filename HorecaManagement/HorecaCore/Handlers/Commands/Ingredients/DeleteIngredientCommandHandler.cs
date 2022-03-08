using Horeca.Shared.Data;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Ingredients
{
    public class DeleteIngredientCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteIngredientCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand, int>
    {
        private readonly IUnitOfWork _repository;

        public DeleteIngredientCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            _repository.Ingredients.Delete(request.Id);

            await _repository.CommitAsync();

            return request.Id;
        }
    }
}