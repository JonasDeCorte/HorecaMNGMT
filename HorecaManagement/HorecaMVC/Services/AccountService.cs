using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.Tokens;
using Horeca.Shared.Dtos.UserPermissions;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Horeca.MVC.Services
{
    public class AccountService : IAccountService
    {
        private HttpClient httpClient;
        private IConfiguration configuration;
        private ITokenService tokenService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountService(HttpClient httpClient, IConfiguration configuration, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }
        public UserDto CurrentUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<HttpResponseMessage> LoginUser(LoginUserDto user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Account}/" +
                $"{ClassConstants.Login}");
            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            TokenResultDto result = JsonConvert.DeserializeObject<TokenResultDto>(response.Content.ReadAsStringAsync().Result);
            tokenService.SetAccessToken(result.AccessToken);
            tokenService.SetRefreshToken(result.RefreshToken);
            var username = new JwtSecurityTokenHandler().ReadJwtToken(result.AccessToken).Claims.Skip(2).First().Value;
            httpContextAccessor.HttpContext.Response.Cookies.Append("Username", username);

            return response;
        }

        public async Task<HttpResponseMessage> RegisterUser(RegisterUserDto user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Account}/" +
                $"{ClassConstants.Register}");
            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> RegisterAdmin(RegisterUserDto user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Account}/" +
                $"{ClassConstants.RegisterAdmin}");
            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> AddPermissions(MutateUserPermissionsDto model)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Account}/" +
                $"{ClassConstants.UserPermissions}");
            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;

        }

        public async Task<HttpResponseMessage> RemovePermissions(MutateUserPermissionsDto model)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Account}/" +
                $"{ClassConstants.UserPermissions}/{model.UserName}");
            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;

        }

        public async Task<IEnumerable<BaseUserDto>> GetUsers()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Account}/{ClassConstants.User}");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<IEnumerable<BaseUserDto>>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        public async Task<UserDto> GetUserByName(string username)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Account}/{ClassConstants.User}/{username}");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<UserDto>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        public bool Authorize(UserDto user, string permission)
        {
            if (user == null)
            {
                return false;
            } else if (!user.Permissions.Any(item => item.PermissionName == permission))
            {
                return false;
            }
            return true;
        }

        public string GetCurrentUserName()
        {
            return httpContextAccessor.HttpContext?.Request.Cookies["Username"];
        }

        public async Task<UserDto> GetCurrentUser()
        {
            var response = await GetUserByName(GetCurrentUserName());
            if (response == null)
            {
                return null;
            } else
            {
                return response;
            }
        }

        public bool IsLoggedIn()
        {
            if (GetCurrentUserName() == null)
            {
                return false;
            }
            return true;
        }
    }
}
