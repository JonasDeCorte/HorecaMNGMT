using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        public Task<List<Schedule>> GetAvailableRestaurantSchedules(int restaurantId);

        public Task<List<Schedule>> GetRestaurantSchedules(int restaurantId);

        public Task<Schedule> GetScheduleById(int id, int restaurantId);

        public Task<bool> CheckExistingStartTime(int scheduleId, DateTime scheduleDate, DateTime startTime, long restaurantId, string action);
    }
}