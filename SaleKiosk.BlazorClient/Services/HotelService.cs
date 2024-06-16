using Newtonsoft.Json;
using SaleKiosk.SharedKernel.Dto;

namespace SaleKiosk.BlazorClient.Services
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelDto>> GetAll();
    }

    public class HotelService : IHotelService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _saleKioskServerUrl;

        public HotelService(HttpClient httpClient, IConfiguration configuration)
        {
            this._httpClient = httpClient;
            this._configuration = configuration;
            this._saleKioskServerUrl = _configuration.GetSection("SaleKioskServerUrl").Value;
        }

        public async Task<IEnumerable<HotelDto>> GetAll()
        {
            var response = await _httpClient.GetAsync("/hotel");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var hotels = JsonConvert.DeserializeObject<IEnumerable<HotelDto>>(content);
                foreach (var h in hotels)
                {
                    h.ImageUrl = _saleKioskServerUrl + h.ImageUrl;
                }

                return hotels;
            }

            return new List<HotelDto>();
        }
    }


}
