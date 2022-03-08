using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Ingredients
{
    public class CreateIngredientCommand : IRequest<int>
    {
        public MutateIngredientDto Model { get; }

        /// <summary>
        /// We're passing the data to be used by the Handler on the other side of the Mediator as Properties,
        /// assigning them values via constructor. When the Request object is created,
        /// we add data to the Request via the constructor which assigns it to the respective public Properties.
        /// </summary>
        /// <param name="model"></param>
        public CreateIngredientCommand(MutateIngredientDto model)
        {
            Model = model;
        }
    }

    /// <summary>
    /// The Handler WRITEs the Ingredient passed to this via the Property Model inside the CommandRequest
    /// object and returns the Id of the created Ingredient.
    /// The Handler pushes the record into the backend (persistent store)
    /// via a UnitOfWork instance which encapsulates the dbContext object.
    /// </summary>
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, int>

    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<MutateIngredientDto> _validator;

        public CreateIngredientCommandHandler(IUnitOfWork repository, IValidator<MutateIngredientDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request.Model);

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
                Name = request.Model.Name,
                BaseAmount = request.Model.BaseAmount,
                IngredientType = request.Model.IngredientType,
                Unit = request.Model.Unit,
            };
            _repository.Ingredients.Add(entity);

            await _repository.CommitAsync();

            return request.Model.Id;
        }
    }
}