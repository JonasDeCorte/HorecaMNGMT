using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;
using Horeca.Shared.Dtos.Menus;
using MediatR;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class AddMenuMenuCardCommand : IRequest<int>
    {
        public AddMenuMenuCardCommand(MutateMenuMenuCardDto model)
        {
            Model = model;
        }

        public MutateMenuMenuCardDto Model { get; }
    }

    public class AddMenuMenuCardCommandHandler : IRequestHandler<AddMenuMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;
        private readonly IValidator<MutateMenuDto> validator;

        public AddMenuMenuCardCommandHandler(IUnitOfWork repository, IValidator<MutateMenuDto> validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public async Task<int> Handle(AddMenuMenuCardCommand request, CancellationToken cancellationToken)
        {
            var result = validator.Validate(request.Model.Menu);
            var menuCard = repository.MenuCards.GetMenuCardIncludingDependencies(request.Model.MenuCardId);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entity = new Menu
            {
                Name = request.Model.Menu.Name,
                Description = request.Model.Menu.Description,
                Category = request.Model.Menu.Category,
            };

            repository.Menus.Add(entity);

            menuCard.Menus.Add(entity);
            repository.MenuCards.Update(menuCard);

            await repository.CommitAsync();

            return entity.Id;
        }
    }
}