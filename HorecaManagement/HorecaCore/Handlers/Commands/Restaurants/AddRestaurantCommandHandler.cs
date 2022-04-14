using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Restaurants;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Horeca.Core.Handlers.Commands.Restaurants
{
    public class AddRestaurantCommand : IRequest<int>
    {
        public MutateRestaurantDto Model { get; }

        public AddRestaurantCommand(MutateRestaurantDto model)
        {
            Model = model;
        }

        public class AddRestaurantCommandHandler : IRequestHandler<AddRestaurantCommand, int>
        {
            private readonly IUnitOfWork repository;
            private readonly UserManager<ApplicationUser> userManager;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public AddRestaurantCommandHandler(IUnitOfWork repository, UserManager<ApplicationUser> userManager)
            {
                this.repository = repository;
                this.userManager = userManager;
            }

            public async Task<int> Handle(AddRestaurantCommand request, CancellationToken cancellationToken)
            {
                logger.Info("trying to create {object} with request: {@Id}", nameof(Restaurant), request);

                var entity = new Restaurant
                {
                    Name = request.Model.Name,
                };

                ApplicationUser? owner = await userManager.FindByNameAsync(request.Model.OwnerName);

                if (!owner.IsOwner)
                {
                    logger.Error(IsNotOwnerException.Instance);
                    throw new IsNotOwnerException();
                }

                owner.Restaurants.Add(new RestaurantUser
                {
                    Restaurant = entity,
                    User = owner,
                });

                await userManager.UpdateAsync(owner);

                await repository.CommitAsync();

                logger.Info("adding {@object} with id {id}", entity, entity.Id);

                return entity.Id;
            }
        }
    }
}