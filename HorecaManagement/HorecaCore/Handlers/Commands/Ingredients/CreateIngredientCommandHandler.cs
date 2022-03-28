using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Ingredients;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Ingredients
{
    public class CreateIngredientCommand : IRequest<int>
    {
        public MutateIngredientDto Model { get; }

        public CreateIngredientCommand(MutateIngredientDto model)
        {
            Model = model;
        }
    }

    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, int>

    {
        private readonly IUnitOfWork repository;
        private readonly IValidator<MutateIngredientDto> _validator;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public CreateIngredientCommandHandler(IUnitOfWork repository, IValidator<MutateIngredientDto> validator)
        {
            this.repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to create {@object} with Id: {Id}", nameof(Ingredient), request.Model.Id);

            var result = _validator.Validate(request.Model);

            if (!result.IsValid)
            {
                logger.Error("Invalid model with errors: ", result.Errors);

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
                Unit = new Shared.Data.Entities.Unit
                {
                    Name = request.Model.Unit.Name,
                },
            };
            repository.Ingredients.Add(entity);

            await repository.CommitAsync();
            logger.Info("adding {@object} with id {id}", entity, entity.Id);

            return entity.Id;
        }
    }
}