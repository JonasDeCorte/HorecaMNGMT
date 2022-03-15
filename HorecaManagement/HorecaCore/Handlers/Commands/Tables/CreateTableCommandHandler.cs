using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Tables;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Tables
{
    public class CreateTableCommand : IRequest<int>
    {
        public CreateTableDto Model { get; }

        public CreateTableCommand(CreateTableDto model)
        {
            Model = model;
        }

        public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, int>
        {
            private readonly IUnitOfWork repository;
            private readonly IValidator<CreateTableDto> validator;

            public CreateTableCommandHandler(IUnitOfWork repository, IValidator<CreateTableDto> validator)
            {
                this.repository = repository;
                this.validator = validator;
            }

            public async Task<int> Handle(CreateTableCommand request, CancellationToken cancellationToken)
            {
                var result = validator.Validate(request.Model);

                if (!result.IsValid)
                {
                    var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                    throw new InvalidRequestBodyException
                    {
                        Errors = errors
                    };
                }
                var entity = new Table
                {
                    Name = request.Model.Name,
                    AmountOfPeople = request.Model.AmountOfPeople,
                };

                repository.Tables.Add(entity);
                await repository.CommitAsync();

                return entity.Id;
            }
        }
    }
}