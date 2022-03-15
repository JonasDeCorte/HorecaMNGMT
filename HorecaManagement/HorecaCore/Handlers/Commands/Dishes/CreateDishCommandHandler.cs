using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Dishes
{
    public class CreateDishCommand : IRequest<int>
    {
        public MutateDishDto Model { get; }

        public CreateDishCommand(MutateDishDto model)
        {
            Model = model;
        }

        public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
        {
            private readonly IUnitOfWork repository;
            private readonly IValidator<MutateDishDto> validator;
            private static Logger logger = LogManager.GetCurrentClassLogger();

            public CreateDishCommandHandler(IUnitOfWork repository, IValidator<MutateDishDto> validator)
            {
                this.repository = repository;
                this.validator = validator;
            }

            public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
            {
                logger.Info("trying to create {@object} with Id: {Id}", nameof(Dish), request.Model.Id);

                var result = validator.Validate(request.Model);

                if (!result.IsValid)
                {
                    logger.Error("Invalid model with errors: ", result.Errors);

                    var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                    throw new InvalidRequestBodyException
                    {
                        Errors = errors
                    };
                }
                var entity = new Dish
                {
                    Name = request.Model.Name,
                    Category = request.Model.Category,
                    Description = request.Model.Description,
                    DishType = request.Model.DishType,
                };

                repository.Dishes.Add(entity);
                await repository.CommitAsync();

                logger.Info("adding {@object} with id {id}", entity, entity.Id);

                return entity.Id;
            }
        }
    }
}