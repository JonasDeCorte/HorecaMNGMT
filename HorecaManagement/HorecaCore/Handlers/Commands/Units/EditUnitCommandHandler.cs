using Horeca.Shared.Data;
using MediatR;

namespace HorecaCore.Handlers.Commands.Units
{
    public class EditUnitCommand : IRequest<int>
    {
        public Horeca.Shared.Data.Entities.Unit Model { get; }

        public EditUnitCommand(Horeca.Shared.Data.Entities.Unit model)
        {
            Model = model;
        }
    }

    public class EditUnitCommandHandler : IRequestHandler<EditUnitCommand, int>
    {
        private readonly IUnitOfWork _repository;

        public EditUnitCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(EditUnitCommand request, CancellationToken cancellationToken)
        {
            _repository.Units.Update(request.Model);

            await _repository.CommitAsync();

            return request.Model.Id;
        }
    }
}