using Newtonsoft.Json;
using SaleKiosk.BlazorClient.ViewModels;
using SaleKiosk.SharedKernel.Dto;
using System.Text;

namespace SaleKiosk.BlazorClient.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAll();
        Task SubmitOrder(CreateReviewDto review);

    }

    public class ReviewService : IReviewService

    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _saleKioskServerUrl;


        public ReviewService(HttpClient httpClient, IConfiguration configuration)
        {
            this._httpClient = httpClient;
            this._configuration = configuration;
            this._saleKioskServerUrl = _configuration.GetSection("SaleKioskServerUrl").Value;
        }

        public async Task<IEnumerable<ReviewDto>> GetAll()
        {
            var response = await _httpClient.GetAsync("/review");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var reviews = JsonConvert.DeserializeObject<IEnumerable<ReviewDto>>(content);
                return reviews;
            }

            return new List<ReviewDto>();
        }
        public async Task SubmitOrder(CreateReviewDto review)
        {

            var content = JsonConvert.SerializeObject(review);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("review", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else
            {
                throw new Exception(responseResult);
            }
        }
    }


}
