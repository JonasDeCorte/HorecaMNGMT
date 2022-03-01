using FluentValidation;
using Horeca.Core.Exceptions;
using HorecaAPI.Data.Entities;
using HorecaShared.Data;
using HorecaShared.Dtos;
using MediatR;

namespace Horeca.Core.Providers.Handlers.Commands
{
    public class CreateIngredientCommand : IRequest<int>
    {   // holds the information to be added to the database.
        public CreateIngredientDto Model { get; }

        public CreateIngredientCommand(CreateIngredientDto model)
        {
            this.Model = model;
        }
    }

    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, int>

    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateIngredientDto> _validator;

        public CreateIngredientCommandHandler(IUnitOfWork repository, IValidator<CreateIngredientDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            CreateIngredientDto model = request.Model;

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entity = new Ingredient
            {
                Name = model.Name,
            };

            _repository.Ingredients.Add(entity);
            await _repository.CommitAsync();

            return entity.Id;
        }
    }
}