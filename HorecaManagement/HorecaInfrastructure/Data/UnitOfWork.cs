using Horeca.Infrastructure.Data.Repositories;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Repositories;

namespace Horeca.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext context;

        public UnitOfWork(DatabaseContext context)
        {
            this.context = context;
        }

        public IIngredientRepository Ingredients => new IngredientRepository(context);

        public IUnitRepository Units => new UnitRepository(context);

        public IDishRepository Dishes => new DishRepository(context);

        public IMenuRepository Menus => new MenuRepository(context);

        public IMenuCardRepository MenuCards => new MenuCardRepository(context);

        public IPermissionRepository PermissionRepository => new PermissionRepository(context);

        public IUserPermissionRepository UserPermissions => new UserPermissionRepository(context);

        public IRestaurantRepository Restaurants => new RestaurantRepository(context);

        public IRestaurantScheduleRepository RestaurantSchedules => new RestaurantScheduleRepository(context);

        public IBookingDetailRepository BookingDetailRepository => new BookingDetailRepository(context);

        public IBookingRepository BookingRepository => new BookingRepository(context);

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}