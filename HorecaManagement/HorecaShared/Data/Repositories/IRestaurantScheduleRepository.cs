using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IRestaurantScheduleRepository : IRepository<RestaurantSchedule>
    {
        public Task<List<RestaurantSchedule>> GetAvailableRestaurantSchedules(int restaurantId);

        public Task<List<RestaurantSchedule>> GetRestaurantSchedules(int restaurantId);

        public Task<bool> CheckExistingStartTime(int scheduleId, DateTime scheduleDate, DateTime startTime, long restaurantId, string action);
    }
}