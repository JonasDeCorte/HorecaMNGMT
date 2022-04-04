using AutoMapper;
using Horeca.Core.Exceptions;
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
        public int Id { get; } // table Id

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
                logger.Info("trying to return {object} with request: {@req}", nameof(OrderDto), request);

                var table = await context.Tables
                                             .SingleOrDefaultAsync(x => x.Id == request.Id);

                logger.Info("checking if table exists (object} with id: {id}", nameof(Table), request.Id);

                if (table is null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }

                Order? order = await context.Orders
                                               .Include(x => x.OrderLines)
                                               .ThenInclude(x => x.Dish)
                                               .SingleOrDefaultAsync(x => x.Id.Equals(table.OrderId));

                logger.Info("getting order with table Id: {id}", table.Id);

                if (order is null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }

                logger.Info("returning {@object} with id: {id}", order, request.Id);

                var mapped = mapper.Map<OrderLinesByOrderIdDto>(order);
                mapped.TableId = table.Id;

                return mapped;
            }
        }
    }
}