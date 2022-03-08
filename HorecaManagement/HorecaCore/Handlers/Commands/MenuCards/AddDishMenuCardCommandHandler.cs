using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class AddDishMenuCardCommand : IRequest<int>

    {
        public AddDishMenuCardCommand(MutateDishMenuCardDto model)
        {
            Model = model;
        }

        public MutateDishMenuCardDto Model { get; }
    }

    public class AddDishMenuCardCommandHandler : IRequestHandler<AddDishMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;
        private readonly IValidator<MutateDishDto> validator;

        public AddDishMenuCardCommandHandler(IUnitOfWork repository, IValidator<MutateDishDto> validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public async Task<int> Handle(AddDishMenuCardCommand request, CancellationToken cancellationToken)
        {
            var result = validator.Validate(request.Model.Dish);
            var menuCard = repository.MenuCards.GetMenuCardIncludingDependencies(request.Model.MenuCardId);

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

            repository.Dishes.Add(entity);

            menuCard.Dishes.Add(entity);
            repository.MenuCards.Update(menuCard);

            await repository.CommitAsync();

            return entity.Id;
        }
    }
}