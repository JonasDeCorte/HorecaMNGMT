using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;

namespace HorecaCore.Handlers.Commands.Dishes
{
    public class EditDishCommand : IRequest<int>
    {
        public Dish Model { get; }

        public EditDishCommand(Dish model)
        {
            Model = model;
        }
    }

    public class EditDishCommandHandler : IRequestHandler<EditDishCommand, int>
    {
        private readonly IUnitOfWork _repository;

        public EditDishCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(EditDishCommand request, CancellationToken cancellationToken)
        {
            _repository.Dishes.Update(request.Model);

            await _repository.CommitAsync();

            return request.Model.Id;
        }
    }
}