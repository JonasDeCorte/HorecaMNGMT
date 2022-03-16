using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Menus
{
    public class CreateMenuCommand : IRequest<int>
    {
        public MutateMenuDto Model { get; }

        public CreateMenuCommand(MutateMenuDto model)
        {
            Model = model;
        }
    }

    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, int>

    {
        private readonly IUnitOfWork repository;
        private readonly IValidator<MutateMenuDto> _validator;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public CreateMenuCommandHandler(IUnitOfWork repository, IValidator<MutateMenuDto> validator)
        {
            this.repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to create {object} with Id: {Id}", nameof(Menu), request.Model.Id);

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

            var entity = new Menu
            {
                Name = request.Model.Name,
                Description = request.Model.Description,
                Category = request.Model.Category,
            };

            repository.Menus.Add(entity);

            await repository.CommitAsync();

            logger.Info("adding {@object} with id {id}", entity, entity.Id);

            return request.Model.Id;
        }
    }
}