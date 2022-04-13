using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Horeca.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        private readonly DatabaseContext context;

        public ScheduleRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> CheckExistingStartTime(int scheduleId, DateTime scheduleDate, DateTime startTime, long restaurantId, string action)
        {
            var schedules = await context.Schedules.Where(rs => rs.RestaurantId == restaurantId).ToListAsync();

            foreach (var schedule in schedules)
            {
                if (action.Equals("update"))
                {
                    if (scheduleId != schedule.Id &&
                    schedule.ScheduleDate.ToString("dd/MM/yyyy") == scheduleDate.ToString("dd/MM/yyyy") &&
                    schedule.StartTime.ToString("hh:mm tt") == startTime.ToString("hh:mm tt"))
                        return true;
                }
                else
                {
                    if (schedule.ScheduleDate.ToString("dd/MM/yyyy") == scheduleDate.ToString("dd/MM/yyyy") &&
                        schedule.StartTime.ToString("hh:mm tt") == startTime.ToString("hh:mm tt"))
                        return true;
                }
            }
            return false;
        }

        public async Task<List<Schedule>> GetAvailableRestaurantSchedules(int restaurantId)
        {
            return await context.Schedules
                .Where(rs => rs.RestaurantId == restaurantId && rs.Status == Constants.ScheduleStatus.Available)
                .ToListAsync();
        }

        public async Task<List<Schedule>> GetRestaurantSchedules(int restaurantId)
        {
            return await context.Schedules
                .Where(rs => rs.RestaurantId == restaurantId)
                .ToListAsync();
        }
    }
}