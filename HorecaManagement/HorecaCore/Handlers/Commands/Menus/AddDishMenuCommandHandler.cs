using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Menus;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Menus
{
    public class AddDishMenuCommand : IRequest<int>
    {
        public AddDishMenuCommand(MutateDishMenuDto model)
        {
            Model = model;
        }

        public MutateDishMenuDto Model { get; }
    }

    public class AddDishMenuCommandHandler : IRequestHandler<AddDishMenuCommand, int>
    {
        private readonly IUnitOfWork repository;
        private readonly IValidator<MutateDishDto> _validator;

        public AddDishMenuCommandHandler(IUnitOfWork repository, IValidator<MutateDishDto> validator)
        {
            this.repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(AddDishMenuCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request.Model.Dish);
            var menu = repository.Menus.GetMenuIncludingDependencies(request.Model.Id);

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
                Name = request.Model.Dish.Name,
                Category = request.Model.Dish.Category,
                Description = request.Model.Dish.Description,
                DishType = request.Model.Dish.DishType,
            };

            menu.Dishes.Add(entity);
            repository.Dishes.Add(entity);
            repository.Menus.Update(menu);
            await repository.CommitAsync();

            return entity.Id;
        }
    }
}