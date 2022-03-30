using Horeca.Shared;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.UserPermissions;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<HttpResponseMessage> LoginUser(LoginUserDto user);
        public Task<HttpResponseMessage> RegisterUser(RegisterUserDto user);
        public Task<HttpResponseMessage> RegisterAdmin(RegisterUserDto user);
        public Task<HttpResponseMessage> UpdatePermissions(MutateUserPermissionsDto model);
        public Task<IEnumerable<BaseUserDto>> GetUsers();
        public Task<UserDto> GetUserByName(string username);
    }
}
