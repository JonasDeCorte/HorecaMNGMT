using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class CreateMenuCardCommand : IRequest<int>
    {
        public CreateMenuCardCommand(MutateMenuCardDto model)
        {
            Model = model;
        }

        public MutateMenuCardDto Model { get; }
    }

    public class CreateMenuCardCommandHandler : IRequestHandler<CreateMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;
        private readonly IValidator<MutateMenuCardDto> validator;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public CreateMenuCardCommandHandler(IUnitOfWork repository, IValidator<MutateMenuCardDto> validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public async Task<int> Handle(CreateMenuCardCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to create {object} with Id: {Id}", nameof(MenuCard), request.Model.Id);

            var result = validator.Validate(request.Model);

            if (!result.IsValid)
            {
                logger.Error("Invalid model with errors: ", result.Errors);

                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entity = new MenuCard
            {
                Name = request.Model.Name,
            };

            repository.MenuCards.Add(entity);

            await repository.CommitAsync();
            logger.Info("adding {@object} with id {id}", entity, entity.Id);

            return entity.Id;
        }
    }
}