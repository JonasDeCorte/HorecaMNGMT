using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Accounts;
using Newtonsoft.Json;

namespace Horeca.MVC.Services
{
    public class PermissionService : IPermissionService
    {
        private HttpClient httpClient;
        private IConfiguration configuration;
        private readonly IAccountService accountService;

        public PermissionService(HttpClient httpClient, IConfiguration configuration, IAccountService accountService)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.accountService = accountService;
        }

        public async Task<List<PermissionDto>> GetPermissions()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Permission}");
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<List<PermissionDto>>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        public async Task<PermissionDto> GetPermissionById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Permission}/{id}");
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<PermissionDto>(response.Content.ReadAsStringAsync().Result);
            return result;
        }
    }
}
