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
        public AddOrderCommand(MutateOrderDto model, int tableId)
        {
            Model = model;
            TableId = tableId;
        }

        public MutateOrderDto Model { get; }
        public int TableId { get; }
    }

    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddOrderCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            logger.Info("trying to create {object} with request: {@Id}", nameof(Order), request);

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

        private static void ValidateModelIds(AddOrderCommand request)
        {
            if (request.Model.TableId == 0)
            {
                request.Model.TableId = request.TableId;
            }
        }
    }
}