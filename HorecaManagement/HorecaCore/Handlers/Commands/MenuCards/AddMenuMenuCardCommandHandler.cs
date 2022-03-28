using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;

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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddMenuMenuCardCommandHandler(IUnitOfWork repository, IValidator<MutateMenuDto> validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public async Task<int> Handle(AddMenuMenuCardCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to add {@object} to menucard with Id: {Id}", request.Model.Menu, request.Model.MenuCardId);

            var result = validator.Validate(request.Model.Menu);
            var menuCard = repository.MenuCards.GetMenuCardIncludingDependencies(request.Model.MenuCardId);

            if (!result.IsValid)
            {
                logger.Error("Invalid model with errors: ", result.Errors);

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
            logger.Info("succes adding {@object} to menucard with id {id}", entity, menuCard.Id);

            return entity.Id;
        }
    }
}