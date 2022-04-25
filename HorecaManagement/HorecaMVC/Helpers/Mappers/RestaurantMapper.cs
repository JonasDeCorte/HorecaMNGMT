using Horeca.MVC.Models.Restaurants;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.Restaurants;

namespace Horeca.MVC.Helpers.Mappers
{
    public static class RestaurantMapper
    {
        public static RestaurantViewModel MapRestaurantModel(RestaurantDto restaurantDto)
        {
            return new RestaurantViewModel()
            {
                Id = restaurantDto.Id,
                Name = restaurantDto.Name,
            };
        }

        public static RestaurantListViewModel MapRestaurantListModel(IEnumerable<RestaurantDto> restaurantDtos)
        {
            RestaurantListViewModel model = new();
            foreach (var restaurantDto in restaurantDtos)
            {
                RestaurantViewModel restaurantModel = MapRestaurantModel(restaurantDto);
                model.Restaurants.Add(restaurantModel);
            }
            return model;
        }

        public static RestaurantDetailViewModel MapRestaurantDetailModel(DetailRestaurantDto restaurantDto)
        {
            RestaurantDetailViewModel restaurantDetailModel = new()
            {
                Id = restaurantDto.Id,
                Name = restaurantDto.Name,
            };
            restaurantDetailModel.ScheduleList = ScheduleMapper.MapScheduleList(restaurantDto.Schedules);
            restaurantDetailModel.Employees = AccountMapper.MapUserModelList(restaurantDto.Employees);
            return restaurantDetailModel;
        }

        public static MutateRestaurantViewModel MapMutateRestaurantModel(DetailRestaurantDto dto)
        {
            return new MutateRestaurantViewModel()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static MutateEmployeeViewModel MapAddEmployeeModel(IEnumerable<BaseUserDto> employees, DetailRestaurantDto restaurant)
        {
            MutateEmployeeViewModel model = new MutateEmployeeViewModel();
            foreach (var employee in employees)
            {
                if (!restaurant.Employees.Any(x => x.Id == employee.Id))
                {
                    var employeeModel = AccountMapper.MapUserModel(employee);
                    model.Employees.Add(employeeModel);
                }
            }
            return model;
        }

        public static MutateRestaurantDto MapCreateRestaurantDto(MutateRestaurantViewModel model)
        {
            return new MutateRestaurantDto()
            {
                Id = model.Id,
                Name = model.Name,
                OwnerName = model.OwnerName,
            };
        }

        public static EditRestaurantDto MapEditRestaurantDto(RestaurantViewModel model)
        {
            return new EditRestaurantDto()
            {
                Id = model.Id,
                Name = model.Name,
            };
        }
    }
}