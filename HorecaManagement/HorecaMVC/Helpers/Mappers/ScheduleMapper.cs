using Horeca.MVC.Models.Schedules;
using Horeca.Shared.Dtos.Bookings;
using Horeca.Shared.Dtos.Schedules;

namespace Horeca.MVC.Helpers.Mappers
{
    public class ScheduleMapper
    {
        private static ScheduleViewModel MapScheduleModel(ScheduleDto scheduleDto)
        {
            ScheduleViewModel scheduleViewModel = new()
            {
                Id = scheduleDto.Id,
                RestaurantId = scheduleDto.Id,
                ScheduleDate = scheduleDto.ScheduleDate,
                StartTime = scheduleDto.StartTime,
                EndTime = scheduleDto.EndTime,
                Capacity = scheduleDto.Capacity,
                AvailableSeat = scheduleDto.AvailableSeat,
                Status = scheduleDto.Status
            };
            return scheduleViewModel;
        }

        public static ScheduleListViewModel MapScheduleList(IEnumerable<ScheduleDto> schedules)
        {
            ScheduleListViewModel list = new();
            foreach (var scheduleDto in schedules)
            {
                ScheduleViewModel model = MapScheduleModel(scheduleDto);
                list.Schedules.Add(model);
            }
            return list;
        }

        public static ScheduleDetailViewModel MapScheduleDetailModel(
            ScheduleByIdDto scheduleByIdDto, IEnumerable<BookingDetailOnlyBookingsDto> scheduleBookings)
        {
            ScheduleDetailViewModel model = new ScheduleDetailViewModel()
            {
                Id = scheduleByIdDto.Id,
                AvailableSeat = scheduleByIdDto.AvailableSeat,
                Capacity = scheduleByIdDto.Capacity,
                EndTime = scheduleByIdDto.EndTime,
                StartTime = scheduleByIdDto.StartTime,
                ScheduleDate = scheduleByIdDto.ScheduleDate,
                RestaurantId = scheduleByIdDto.RestaurantId,
                RestaurantName = scheduleByIdDto.RestaurantName,
                Status = scheduleByIdDto.Status,
            };
            foreach (var scheduleBooking in scheduleBookings)
            {
                var booking = BookingMapper.MapBookingModel(scheduleBooking);
                model.Bookings.Add(booking);
            }
            return model;
        }

        public static MutateScheduleDto MapMutateScheduleDto(MutateScheduleViewModel model)
        {
            return new MutateScheduleDto()
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

        public static MutateScheduleViewModel MapMutateScheduleModel(ScheduleByIdDto model)
        {
            return new MutateScheduleViewModel()
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