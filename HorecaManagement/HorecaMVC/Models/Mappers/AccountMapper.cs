using Horeca.MVC.Models.Accounts;
using Horeca.Shared.Dtos.Accounts;

namespace Horeca.MVC.Models.Mappers
{
    public static class AccountMapper
    {
        public static UserViewModel MapUserModel(BaseUserDto userDto)
        {
            UserViewModel userViewModel = new UserViewModel
            {
                Id = userDto.Id,
                Username = userDto.Username
            };
            return userViewModel;
        }
        public static UserPermissionsViewModel MapUserPermissionsModel(UserDto userDto)
        {
            UserPermissionsViewModel userPermissionsModel = new UserPermissionsViewModel
            {
                Id = userDto.Id,
                Username = userDto.Username
            };
            foreach (var permission in userDto.Permissions)
            {
                userPermissionsModel.Permissions.Add(permission);
            }
            return userPermissionsModel;
        }

        public static LoginUserDto MapLoginUser(LoginUserViewModel userModel)
        {
            LoginUserDto result = new LoginUserDto
            {
                Username = userModel.Username,
                Password = userModel.Password
            };
            return result;
        }

        public static RegisterUserDto MapRegisterUser(RegisterUserViewModel userModel)
        {
            RegisterUserDto result = new RegisterUserDto
            {
                Username = userModel.Username,
                Email = userModel.Email,
                Password = userModel.Password
            };
            return result;
        }
    }
}
