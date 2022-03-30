using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Tables;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Tables
{
    public class AddTablesForRestaurantScheduleCommand : IRequest<IEnumerable<TableDto>>
    {
        public AddTablesForRestaurantScheduleCommand(int restaurantScheduleId)
        {
            RestaurantScheduleId = restaurantScheduleId;
        }

        public int RestaurantScheduleId { get; }
    }

    public class AddTablesForRestaurantScheduleCommandHandler : IRequestHandler<AddTablesForRestaurantScheduleCommand, IEnumerable<TableDto>>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddTablesForRestaurantScheduleCommandHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TableDto>> Handle(AddTablesForRestaurantScheduleCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to create {object} with request: {@Id}", nameof(Table), request);

            var bookingDetails = await repository.BookingDetails.GetDetailsForRestaurantSchedule(request.RestaurantScheduleId);
            foreach (var bookingDetail in bookingDetails)
            {
                Table table = new()
                {
                    RestaurantScheduleId = bookingDetail.RestaurantScheduleId,
                    Pax = bookingDetail.Pax,
                    RestaurantSchedule = bookingDetail.RestaurantSchedule
                };
                logger.Info("adding {@object} with id {id}", table, table.Id);
                repository.Tables.Add(table);
            }
            repository.CommitAsync();

            var bookedTables = repository.Tables.GetAll().Where(x => x.RestaurantScheduleId.Equals(request.RestaurantScheduleId));

            return mapper.Map<IEnumerable<TableDto>>(bookedTables);
        }
    }
}