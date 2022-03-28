using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Units;
using MediatR;
using NLog;

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
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public CreateUnitCommandHandler(IUnitOfWork repository, IValidator<MutateUnitDto> validator)
            {
                this.repository = repository;
                _validator = validator;
            }

            public async Task<int> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
            {
                logger.Info("trying to create {@object} with Id: {Id}", nameof(Shared.Data.Entities.Unit), request.Model.Id);

                var result = _validator.Validate(request.Model);

                if (!result.IsValid)
                {
                    logger.Error("Invalid model with errors: ", result.Errors);
                    var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                    throw new InvalidRequestBodyException
                    {
                        Errors = errors
                    };
                }
                var entity = new Shared.Data.Entities.Unit
                {
                    Name = request.Model.Name,
                };
                repository.Units.Add(entity);
                await repository.CommitAsync();
                logger.Info("adding {@unit} with id {id}", entity, entity.Id);

                return entity.Id;
            }
        }
    }
}