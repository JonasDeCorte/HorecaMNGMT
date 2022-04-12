using Horeca.MVC.Models.Schedules;
using Horeca.Shared.Dtos.RestaurantSchedules;

namespace Horeca.MVC.Models.Mappers
{
    public class ScheduleMapper
    {
        private static RestaurantScheduleViewModel MapRestaurantScheduleModel(RestaurantScheduleDto restaurantScheduleDto)
        {
            RestaurantScheduleViewModel restaurantScheduleViewModel = new()
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

        public static RestaurantScheduleListViewModel MapRestaurantScheduleList(IEnumerable<RestaurantScheduleDto> restaurantSchedules)
        {
            RestaurantScheduleListViewModel list = new();
            foreach (var restaurantScheduleDto in restaurantSchedules)
            {
                RestaurantScheduleViewModel model = MapRestaurantScheduleModel(restaurantScheduleDto);
                list.RestaurantSchedules.Add(model);
            }
            return list;
        }

        public static RestaurantScheduleDetailViewModel MapRestaurantScheduleDetailModel(RestaurantScheduleByIdDto restaurantScheduleByIdDto)
        {
            return new RestaurantScheduleDetailViewModel()
            {
                Id = restaurantScheduleByIdDto.Id,
                AvailableSeat = restaurantScheduleByIdDto.AvailableSeat,
                Capacity = restaurantScheduleByIdDto.Capacity,
                EndTime = restaurantScheduleByIdDto.EndTime,
                StartTime = restaurantScheduleByIdDto.StartTime,
                ScheduleDate = restaurantScheduleByIdDto.ScheduleDate,
                RestaurantId = restaurantScheduleByIdDto.RestaurantId,
                RestaurantName = restaurantScheduleByIdDto.RestaurantName,
                Status = restaurantScheduleByIdDto.Status,
            };
        }

        public static MutateRestaurantScheduleDto MapCreateRestaurantScheduleDto(MutateRestaurantScheduleViewModel model)
        {
            return new MutateRestaurantScheduleDto()
            {
                Id = model.Id,
                AvailableSeat = model.AvailableSeat,
                Capacity = model.Capacity,
                EndTime = model.EndTime,
                RestaurantId = model.RestaurantId,
                ScheduleDate = model.ScheduleDate,
                StartTime = model.StartTime,
                Status = model.Status
            };
        }
    }
}