using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;
using NLog;

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
        private readonly IUnitOfWork repository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteIngredientCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(Ingredient), request.Id);

            repository.Ingredients.Delete(request.Id);

            logger.Info("deleted {object} with Id: {id}", nameof(Ingredient), request.Id);

            await repository.CommitAsync();

            return request.Id;
        }
    }
}