﻿using Horeca.MVC.Models.Accounts;
using Horeca.Shared.Dtos.Accounts;

namespace Horeca.MVC.Models.Mappers
{
    public static class AccountMapper
    {
        public static LoginUserDto MapLoginUser(UserViewModel userModel)
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
