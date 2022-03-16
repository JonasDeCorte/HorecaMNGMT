using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IAccountService
    {
        public void LoginUser(LoginUserDto user);
        public void RegisterUser(RegisterUserDto user);
        public void RegisterAdmin(RegisterUserDto user);
        public void AddUserRole(string username, MutateRolesUserDto model);
        public void DeleteUserRole(string username, MutateRolesUserDto model);
        public Task<IEnumerable<UserDto>> GetUsers();
        public Task<UserDto> GetUserByName(string username);
    }
}
