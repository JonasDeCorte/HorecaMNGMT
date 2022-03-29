using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.MenuCards;
using Horeca.Shared.Dtos.Restaurants;
using Horeca.Shared.Dtos.RestaurantSchedules;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Restaurants
{
    public class GetRestaurantByIdQuery : IRequest<DetailRestaurantDto>
    {
        public GetRestaurantByIdQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public int RestaurantId { get; }
    }

    public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, DetailRestaurantDto>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetRestaurantByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<DetailRestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to return {object} with id: {id}", nameof(DetailRestaurantDto), request.RestaurantId);

            var restaurant = await Task.FromResult(repository.Restaurants.GetRestaurantIncludingDependenciesById(request.RestaurantId));
            List<Shared.Data.Entities.RestaurantSchedule>? restaurantSchedules = await repository.RestaurantSchedules.GetRestaurantSchedules(restaurant.Id);

            if (restaurant is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            logger.Info("returning {@object} with id: {id}", restaurant, restaurant.Id);

            return new DetailRestaurantDto()
            {
                Employees = restaurant.Employees.Select(x => new BaseUserDto()
                {
                    Username = x.UserName
                }).ToList(),
                Id = restaurant.Id,
                MenuCards = restaurant.MenuCards.Select(x => new MenuCardDto()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList(),
                Name = restaurant.Name,
                RestaurantSchedules = (List<RestaurantScheduleDto>)restaurantSchedules.Select(x => new RestaurantScheduleDto()
                {
                    RestaurantId = x.Restaurant.Id,
                    AvailableSeat = x.AvailableSeat,
                    Capacity = x.Capacity,
                    EndTime = x.EndTime,
                    ScheduleDate = x.ScheduleDate,
                    StartTime = x.StartTime,
                    Status = x.Status
                }).ToList(),
            };
        }
    }
}