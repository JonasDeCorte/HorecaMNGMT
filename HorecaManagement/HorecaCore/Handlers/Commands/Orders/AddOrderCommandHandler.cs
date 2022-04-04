using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Orders;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Orders
{
    public class AddOrderCommand : IRequest<int>
    {
        public AddOrderCommand(MutateOrderDto model)
        {
            Model = model;
        }

        public MutateOrderDto Model { get; }
    }

    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, int>
    {
        private readonly IUnitOfWork repository;
        private readonly IValidator<MutateOrderDto> validator;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddOrderCommandHandler(IUnitOfWork repository, IValidator<MutateOrderDto> validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public async Task<int> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to create {object} with request: {@Id}", nameof(Order), request);

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
            Receipt receipt = new();

            foreach (var orderDishDto in request.Model.Dishes)
            {
                var DbDish = repository.Dishes.Get(orderDishDto.Id);
                receipt.AddItem(DbDish, orderDishDto.Quantity);
            }

            logger.Info("creating order with receipt {@object} ", receipt);

            Order? order = await repository.Orders.CreateOrder(receipt, request.Model.TableId);
            if (order == null)
            {
                logger.Error(EntityNotFoundException.Instance);
                throw new EntityNotFoundException();
            }
            logger.Info("returning order {@object} with id: {id}", order, order.Id);

            await repository.CommitAsync();

            return order.Id;
        }
    }
}