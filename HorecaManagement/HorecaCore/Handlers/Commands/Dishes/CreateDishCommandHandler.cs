using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using MediatR;

namespace HorecaCore.Handlers.Commands.Dishes
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
            private readonly IUnitOfWork _repository;
            private readonly IValidator<MutateDishDto> _validator;

            public CreateDishCommandHandler(IUnitOfWork repository, IValidator<MutateDishDto> validator)
            {
                _repository = repository;
                _validator = validator;
            }

            public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
            {
                MutateDishDto model = request.Model;

                var result = _validator.Validate(model);

                if (!result.IsValid)
                {
                    var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                    throw new InvalidRequestBodyException
                    {
                        Errors = errors
                    };
                }
                var entity = new Dish
                {
                    Name = model.Name,
                    Category = model.Category,
                    Description = model.Description,
                    Ingredients = model.Ingredients,
                    DishType = model.DishType,
                };

                _repository.Dishes.Add(entity);
                await _repository.CommitAsync();

                return entity.Id;
            }
        }
    }
}