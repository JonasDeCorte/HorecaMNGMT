using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Units;
using MediatR;

namespace HorecaCore.Handlers.Commands
{
    public class CreateUnitCommand : IRequest<int>
    {
        public CreateUnitDto Model { get; }

        public CreateUnitCommand(CreateUnitDto model)
        {
            Model = model;
        }

        public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, int>
        {
            private readonly IUnitOfWork _repository;
            private readonly IValidator<CreateUnitDto> _validator;

            public CreateUnitCommandHandler(IUnitOfWork repository, IValidator<CreateUnitDto> validator)
            {
                _repository = repository;
                _validator = validator;
            }

            public async Task<int> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
            {
                CreateUnitDto model = request.Model;

                var result = _validator.Validate(model);

                if (!result.IsValid)
                {
                    var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                    throw new InvalidRequestBodyException
                    {
                        Errors = errors
                    };
                }
                var entity = new Horeca.Shared.Data.Entities.Unit
                {
                    Name = model.Name,
                };

                _repository.Units.Add(entity);
                await _repository.CommitAsync();

                return entity.Id;
            }
        }
    }
}