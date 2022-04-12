using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Data.Services;
using Horeca.Shared.Dtos.Restaurants;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;
using System.Linq;

namespace Horeca.Core.Handlers.Queries.Restaurants
{
    public class GetRestaurantByUserIdQuery : IRequest<List<RestaurantDto>>
    {
        public GetRestaurantByUserIdQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class GetRestaurantByUserIdQueryHandler : IRequestHandler<GetRestaurantByUserIdQuery, List<RestaurantDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetRestaurantByUserIdQueryHandler(IApplicationDbContext context, IUnitOfWork repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.repository = repository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<List<RestaurantDto>> Handle(GetRestaurantByUserIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to return {object}", nameof(RestaurantDto));

            var user = await userManager.FindByIdAsync(request.UserId);

            if (user is null)
            {
                logger.Error(UserNotFoundException.Instance);

                throw new UserNotFoundException();
            }
            List<Restaurant> restos = new();
            List<RestaurantUser>? restaurantUsers = context.RestaurantUsers.Where(x => x.UserId == user.Id).ToList();

            if (user.IsOwner)
            {
                logger.Info("user with id: {id }is owner ", user.Id);
                GetUserRestaurants(restos, restaurantUsers);
                return restos.Select(x => new RestaurantDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }
            if (!user.IsOwner && restaurantUsers.Count == 0)
            {
                logger.Info("user with id: {id }is not an owner or employee ", user.Id);

                return await Task.FromResult(repository.Restaurants.GetAll().Select(x => new RestaurantDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList());
            }

            logger.Info("user with id: {id }is an employee ", user.Id);
            GetUserRestaurants(restos, restaurantUsers);

            return restos.Select(x => new RestaurantDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        private void GetUserRestaurants(List<Restaurant> restos, List<RestaurantUser> restaurantUsers)
        {
            restos.AddRange(from item in restaurantUsers
                            let resto = repository.Restaurants.Get(item.RestaurantId)
                            select resto);
        }
    }
}