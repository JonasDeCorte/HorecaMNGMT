using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class EditMenuCardCommand : IRequest<int>
    {
        public EditMenuCardCommand(MutateMenuCardDto model)
        {
            Model = model;
        }

        public MutateMenuCardDto Model { get; }
    }

    public class EditMenuCardCommandHandler : IRequestHandler<EditMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;

        public EditMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditMenuCardCommand request, CancellationToken cancellationToken)
        {
            var menuCard = repository.MenuCards.Get(request.Model.Id);

            if (menuCard is null)
            {
                throw new EntityNotFoundException("Entity does not exist");
            }

            menuCard.Name = request.Model.Name ?? menuCard.Name;

            repository.MenuCards.Update(menuCard);
            await repository.CommitAsync();

            return request.Model.Id;
        }
    }
}