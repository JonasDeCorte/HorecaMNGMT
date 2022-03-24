using Horeca.MVC.Services.Interfaces;
using Horeca.Shared;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.UserPermissions;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Horeca.MVC.Services
{
    public class AccountService : IAccountService
    {
        private HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        private IConfiguration configuration;

        public AccountService(HttpClient httpClient, IConfiguration IConfig, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
            configuration = IConfig;
        }
        public void Authorizer()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                httpContextAccessor.HttpContext.Session.GetString("JWToken"));
        }

        public async Task<string> LoginUser(LoginUserDto user)
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
            LoginResult result = JsonConvert.DeserializeObject<LoginResult>(response.Content.ReadAsStringAsync().Result);
            return result.AccessToken;
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

        public async Task<HttpResponseMessage> UpdatePermissions(MutateUserPermissionsDto model)
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

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Account}/{ClassConstants.User}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        public async Task<UserDto> GetUserByName(string username)
        {
            Authorizer();
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Account}/{ClassConstants.User}/{username}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = JsonConvert.DeserializeObject<UserDto>(response.Content.ReadAsStringAsync().Result);

            return result;
        }
    }
}
