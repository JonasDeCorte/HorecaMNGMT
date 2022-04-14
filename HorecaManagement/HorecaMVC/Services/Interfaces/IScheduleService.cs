using Horeca.Shared.Dtos.Schedules;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task<IEnumerable<ScheduleDto>> GetSchedules(int restaurantId);
        public Task<ScheduleByIdDto> GetScheduleById(int id);
        public Task<HttpResponseMessage> AddSchedule(MutateScheduleDto scheduleDto);
        public Task<HttpResponseMessage> UpdateSchedule(MutateScheduleDto scheduleDto);
        public Task<HttpResponseMessage> DeleteSchedule(int id);
    }
}
