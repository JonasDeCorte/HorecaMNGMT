using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Units;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Units
{
    public class CreateUnitCommand : IRequest<int>
    {
        public MutateUnitDto Model { get; }

        public CreateUnitCommand(MutateUnitDto model)
        {
            Model = model;
        }

        public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, int>
        {
            private readonly IUnitOfWork repository;
            private readonly IValidator<MutateUnitDto> _validator;

            public CreateUnitCommandHandler(IUnitOfWork repository, IValidator<MutateUnitDto> validator)
            {
                this.repository = repository;
                _validator = validator;
            }

            public async Task<int> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
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
                var entity = new Horeca.Shared.Data.Entities.Unit
                {
                    Name = request.Model.Name,
                };

                repository.Units.Add(entity);
                await repository.CommitAsync();

                return entity.Id;
            }
        }
    }
}