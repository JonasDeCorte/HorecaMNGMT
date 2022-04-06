using Horeca.MVC.Models.Schedules;
using Horeca.Shared.Dtos.RestaurantSchedules;

namespace Horeca.MVC.Models.Mappers
{
    public class ScheduleMapper
    {
        private static RestaurantScheduleViewModel MapRestaurantScheduleModel(RestaurantScheduleDto restaurantScheduleDto)
        {
            RestaurantScheduleViewModel restaurantScheduleViewModel = new RestaurantScheduleViewModel
            {
                Id = restaurantScheduleDto.Id,
                RestaurantId = restaurantScheduleDto.Id,
                ScheduleDate = restaurantScheduleDto.ScheduleDate,
                StartTime = restaurantScheduleDto.StartTime,
                EndTime = restaurantScheduleDto.EndTime,
                Capacity = restaurantScheduleDto.Capacity,
                AvailableSeat = restaurantScheduleDto.AvailableSeat,
                Status = restaurantScheduleDto.Status
            };
            return restaurantScheduleViewModel;
        }

        public static List<RestaurantScheduleViewModel> MapRestaurantScheduleList(List<RestaurantScheduleDto> restaurantSchedules)
        {
            List<RestaurantScheduleViewModel> list = new List<RestaurantScheduleViewModel>();
            foreach (var restaurantScheduleDto in restaurantSchedules)
            {
                RestaurantScheduleViewModel model = MapRestaurantScheduleModel(restaurantScheduleDto);
                list.Add(model);
            }
            return list;
        }

    }
}
