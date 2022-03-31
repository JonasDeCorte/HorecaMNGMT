using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
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
            if (restaurant is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            logger.Info("returning {@object} with id: {id}", restaurant, restaurant.Id);

            return await MapDetailRestaurant(restaurant, new DetailRestaurantDto());
        }

        private async Task<DetailRestaurantDto> MapDetailRestaurant(Restaurant? restaurant, DetailRestaurantDto dto)
        {
            dto.Id = restaurant.Id;
            dto.Name = restaurant.Name;

            foreach (RestaurantUser? item in restaurant.Employees)
            {
                dto.Employees.Add(new BaseUserDto()
                {
                    Id = item.UserId,
                    Username = item.User.UserName
                });
            }
            foreach (MenuCard? item in restaurant.MenuCards)
            {
                dto.MenuCards.Add(new MenuCardDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }

            List<RestaurantSchedule>? restaurantSchedules = await repository.RestaurantSchedules.GetRestaurantSchedules(restaurant.Id);
            if (restaurantSchedules.Count != 0)
            {
                dto.RestaurantSchedules = restaurantSchedules.Select(x => new RestaurantScheduleDto()
                {
                    Id = x.Id,
                    RestaurantId = restaurant.Id,
                    AvailableSeat = x.AvailableSeat,
                    Capacity = x.Capacity,
                    EndTime = x.EndTime,
                    ScheduleDate = x.ScheduleDate,
                    StartTime = x.StartTime,
                    Status = x.Status
                }).ToList();
            }

            return dto;
        }
    }
}