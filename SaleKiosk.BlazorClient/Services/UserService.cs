using Newtonsoft.Json;
using SaleKiosk.SharedKernel.Dto;

namespace SaleKiosk.BlazorClient.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _staybookServerUrl;

        public UserService(HttpClient httpClient, IConfiguration configuration)
        {
            this._httpClient = httpClient;
            this._configuration = configuration;
            this._staybookServerUrl = _configuration.GetSection("SaleKioskServerUrl").Value;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var response = await _httpClient.GetAsync("/user");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(content);
                foreach (var u in users)
                {
                    u.ImageUrl = _staybookServerUrl + u.ImageUrl;
                }

                return users;
            }

            return new List<UserDto>();
        }
    }


}