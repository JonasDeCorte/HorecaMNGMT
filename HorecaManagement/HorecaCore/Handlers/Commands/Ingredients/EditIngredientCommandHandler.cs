using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using MediatR;

namespace HorecaCore.Handlers.Commands.Ingredients
{
    public class EditIngredientCommand : IRequest<int>
    {
        public Ingredient Model { get; }

        public EditIngredientCommand(Ingredient model)
        {
            Model = model;
        }
    }

    public class EditIngredientCommandHandler : IRequestHandler<EditIngredientCommand, int>
    {
        private readonly IUnitOfWork _repository;

        public EditIngredientCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(EditIngredientCommand request, CancellationToken cancellationToken)
        {
            _repository.Ingredients.Update(request.Model);

            await _repository.CommitAsync();

            return request.Model.Id;
        }
    }
}