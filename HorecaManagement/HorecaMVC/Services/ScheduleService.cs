using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.RestaurantSchedules;

namespace Horeca.MVC.Services
{
    public class ScheduleService : IScheduleService
    {
        public Task<RestaurantScheduleByIdDto> GetRestaurantScheduleById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestaurantScheduleDto>> GetRestaurantSchedules(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> AddRestaurantSchedule(MutateRestaurantScheduleDto restaurantScheduleDto)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> UpdateRestaurantSchedule(MutateRestaurantScheduleDto restaurantScheduleDto)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeleteRestaurantSchedule(int id)
        {
            throw new NotImplementedException();
        }
    }
}
