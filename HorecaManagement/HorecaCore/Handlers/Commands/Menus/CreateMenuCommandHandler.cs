using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Menus;
using MediatR;

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
        private readonly IUnitOfWork _repository;
        private readonly IValidator<MutateMenuDto> _validator;

        public CreateMenuCommandHandler(IUnitOfWork repository, IValidator<MutateMenuDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
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

            var entity = new Menu
            {
                Name = request.Model.Name,
                Description = request.Model.Description,
                Category = request.Model.Category,
            };

            _repository.Menus.Add(entity);

            await _repository.CommitAsync();

            return request.Model.Id;
        }
    }
}