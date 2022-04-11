using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.Tokens;
using Horeca.Shared.Dtos.UserPermissions;
using Newtonsoft.Json;
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

        public  UserDto CurrentUser { get; set; }

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

            //var test = httpContextAccessor.HttpContext.Request.Cookies["JWToken"];
            //var test2 = httpContextAccessor.HttpContext.Request.Cookies["RefreshToken"];
            var test = httpContextAccessor.HttpContext.Session.GetString("JWToken");
            var test2 = httpContextAccessor.HttpContext.Session.GetString("RefreshToken");

            UserDto currentUser = await GetUserByName(user.Username);
            if (currentUser != null)
            {
                //httpContextAccessor.HttpContext.Response.Cookies.Delete("CurrentUser");
                httpContextAccessor.HttpContext.Session.Remove("CurrentUser");
            }
            //httpContextAccessor.HttpContext.Response.Cookies.Append("CurrentUser", JsonConvert.SerializeObject(currentUser));
            httpContextAccessor.HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(currentUser));

            return response;
        }

        public async Task<HttpResponseMessage> LogoutUser()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Account}/" +
                $"{ClassConstants.RefreshToken}/revoke");

            var refreshToken = tokenService.GetRefreshToken();
            request.Content = new StringContent(JsonConvert.SerializeObject(refreshToken), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            foreach(string key in httpContextAccessor.HttpContext.Session.Keys)
            {
                httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
                httpContextAccessor.HttpContext.Session.Remove(key);
            }

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
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Account}/{ClassConstants.User}/{username}");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            } 
            else
            {
                var result = JsonConvert.DeserializeObject<UserDto>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
        }

        public UserDto GetCurrentUser()
        {
            //var userCookie = httpContextAccessor.HttpContext.Request.Cookies["CurrentUser"];
            var userCookie = httpContextAccessor.HttpContext.Session.GetString("CurrentUser");
            if (string.IsNullOrEmpty(userCookie))
            {
                return null;
            }
            var currentUser = JsonConvert.DeserializeObject<UserDto>(userCookie);
            if (currentUser == null)
            {
                return null;
            }
            else
            {
                return currentUser;
            }
        }

        public bool Authorize(string permission)
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null || currentUser.Permissions == null)
            {
                return false;
            } 
            else if (!currentUser.Permissions.Any(item => item.PermissionName == permission))
            {
                return false;
            }
            return true;
        }

        public bool IsLoggedIn()
        {
            if (GetCurrentUser() == null)
            {
                return false;
            }
            return true;
        }
    }
}
