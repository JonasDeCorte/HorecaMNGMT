﻿using Horeca.MVC.Models.Accounts;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.UserPermissions;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                Username = userDto.Username
            };
            foreach (var permission in userDto.Permissions)
            {
                PermissionViewModel model = MapPermissionModel(permission);
                userPermissionsModel.Permissions.Add(model);
            }
            return userPermissionsModel;
        }

        public static PermissionViewModel MapPermissionModel(PermissionDto permission)
        {
            return new PermissionViewModel
            {
                Id = permission.Id,
                PermissionName = permission.PermissionName
            };
        }

        public static List<PermissionViewModel> MapAddPermissionsList(UserPermissionsViewModel user, List<PermissionDto> permissions)
        {
            List<PermissionViewModel> permissionList = new();
            foreach (var permission in permissions)
            {
                PermissionViewModel model = MapPermissionModel(permission);
                if (!user.Permissions.Any(item => item.Id == model.Id))
                {
                    permissionList.Add(model);
                }
            }
            return permissionList;
        }

        public static List<PermissionViewModel> MapRemovePermissionsList(UserPermissionsViewModel userModel)
        {
            List<PermissionViewModel> permissionList = new();
            foreach (var permissionModel in userModel.Permissions)
            {
                permissionList.Add(permissionModel);
            }
            return permissionList;
        }

        public static List<UserViewModel> MapUserModelList(List<BaseUserDto> employees)
        {
            List<UserViewModel> userList = new();
            foreach (BaseUserDto employee in employees)
            {
                userList.Add(MapUserModel(employee));
            }
            return userList;
        }

        public static LoginUserDto MapLoginUser(LoginUserViewModel userModel)
        {
            return new LoginUserDto
            {
                Username = userModel.Username,
                Password = userModel.Password
            };
        }

        public static RegisterUserDto MapRegisterUser(RegisterUserViewModel userModel)
        {
            return new RegisterUserDto
            {
                Username = userModel.Username,
                Email = userModel.Email,
                Password = userModel.Password
            };
        }

        public static MutateUserPermissionsDto MapMutatePermissionsDto(MutatePermissionsViewModel model)
        {
            MutateUserPermissionsDto result = new()
            {
                UserName = model.Username,
                PermissionIds = new List<int>()
            };
            foreach (var id in model.PermissionId)
            {
                result.PermissionIds.Add(id);
            }
            return result;
        }
    }
}