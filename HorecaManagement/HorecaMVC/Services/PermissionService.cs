using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Accounts;
using Newtonsoft.Json;

namespace Horeca.MVC.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public PermissionService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<List<PermissionDto>> GetPermissions()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Permission}");
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<PermissionDto>>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new List<PermissionDto>();
                }
                return result;
            }
            return null;
        }

        public async Task<PermissionDto> GetPermissionById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Permission}/{id}");
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<PermissionDto>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new PermissionDto();
                }
                return result;
            }
            return null;
        }
    }
}