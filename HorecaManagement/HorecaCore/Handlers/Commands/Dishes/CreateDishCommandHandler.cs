using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using MediatR;

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
            private readonly IValidator<MutateDishDto> _validator;

            public CreateDishCommandHandler(IUnitOfWork repository, IValidator<MutateDishDto> validator)
            {
                this.repository = repository;
                _validator = validator;
            }

            public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
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
                var entity = new Dish
                {
                    Name = request.Model.Name,
                    Category = request.Model.Category,
                    Description = request.Model.Description,
                    DishType = request.Model.DishType,
                };

                repository.Dishes.Add(entity);
                await repository.CommitAsync();

                return entity.Id;
            }
        }
    }
}