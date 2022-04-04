using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Services;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace HorecaCore.Handlers.Queries.Orders
{
    public class GetOrderLinesByOrderIdQuery : IRequest<OrderLinesByOrderIdDto>
    {
        public int Id { get; }

        public GetOrderLinesByOrderIdQuery(int id)
        {
            Id = id;
        }

        public class GetOrderLinesByOrderIdQueryHandler : IRequestHandler<GetOrderLinesByOrderIdQuery, OrderLinesByOrderIdDto>
        {
            private readonly IMapper mapper;
            private readonly IApplicationDbContext context;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public GetOrderLinesByOrderIdQueryHandler(IMapper mapper, IApplicationDbContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<OrderLinesByOrderIdDto> Handle(GetOrderLinesByOrderIdQuery request, CancellationToken cancellationToken)
            {
                logger.Info("trying to return {object} with id: {id}", nameof(OrderDto), request.Id);

                Order? order = await context.Orders.Include(x => x.Table)
                                               .Include(x => x.OrderLines)
                                               .ThenInclude(x => x.Dish)
                                               .SingleOrDefaultAsync(x => x.Id.Equals(request.Id));

                if (order is null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }
                OrderLinesByOrderIdDto orderlinesDto = new();
                foreach (var line in order.OrderLines)
                {
                    orderlinesDto.Lines.Add(new OrderLineDto()
                    {
                        Dish = new DishDto()
                        {
                            Price = line.Dish.Price,
                            Id = line.Dish.Id,
                            Category = line.Dish.Category,
                            Description = line.Dish.Description,
                            DishType = line.Dish.DishType,
                            Name = line.Dish.Name
                        },
                        Id = line.Id,
                        Price = line.Price,
                        Quantity = line.Quantity
                    });
                }
                orderlinesDto.TableId = order.Table.Id;
                orderlinesDto.Id = order.Id;

                logger.Info("returning {@object} with id: {id}", order, request.Id);
                return mapper.Map<OrderLinesByOrderIdDto>(order);
            }
        }
    }
}