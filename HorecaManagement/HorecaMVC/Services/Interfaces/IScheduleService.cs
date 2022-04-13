using Horeca.Shared.Dtos.RestaurantSchedules;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task<IEnumerable<RestaurantScheduleDto>> GetRestaurantSchedules(int restaurantId);
        public Task<RestaurantScheduleByIdDto> GetRestaurantScheduleById(int id);
        public Task<HttpResponseMessage> AddRestaurantSchedule(MutateRestaurantScheduleDto restaurantScheduleDto);
        public Task<HttpResponseMessage> UpdateRestaurantSchedule(MutateRestaurantScheduleDto restaurantScheduleDto);
        public Task<HttpResponseMessage> DeleteRestaurantSchedule(int id);
    }
}
