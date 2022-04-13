using Horeca.MVC.Models.Schedules;
using Horeca.Shared.Dtos.Bookings;
using Horeca.Shared.Dtos.RestaurantSchedules;

namespace Horeca.MVC.Models.Mappers
{
    public class ScheduleMapper
    {
        private static RestaurantScheduleViewModel MapRestaurantScheduleModel(ScheduleDto restaurantScheduleDto)
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

        public static RestaurantScheduleListViewModel MapRestaurantScheduleList(IEnumerable<ScheduleDto> restaurantSchedules)
        {
            RestaurantScheduleListViewModel list = new();
            foreach (var restaurantScheduleDto in restaurantSchedules)
            {
                RestaurantScheduleViewModel model = MapRestaurantScheduleModel(restaurantScheduleDto);
                list.RestaurantSchedules.Add(model);
            }
            return list;
        }

        public static RestaurantScheduleDetailViewModel MapRestaurantScheduleDetailModel(
            RestaurantScheduleByIdDto restaurantScheduleByIdDto, IEnumerable<BookingDetailOnlyBookingsDto> scheduleBookings)
        {
            RestaurantScheduleDetailViewModel model = new RestaurantScheduleDetailViewModel()
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
            foreach (var scheduleBooking in scheduleBookings)
            {
                var booking = BookingMapper.MapBookingModel(scheduleBooking);
                model.Bookings.Add(booking);
            }
            return model;
        }

        public static MutateRestaurantScheduleDto MapMutateRestaurantScheduleDto(MutateRestaurantScheduleViewModel model)
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

        public static MutateRestaurantScheduleViewModel MapMutateRestaurantScheduleModel(RestaurantScheduleByIdDto model)
        {
            return new MutateRestaurantScheduleViewModel()
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