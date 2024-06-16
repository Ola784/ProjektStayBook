using Newtonsoft.Json;
using SaleKiosk.BlazorClient.ViewModels;
using SaleKiosk.SharedKernel.Dto;
using System.Text;

namespace SaleKiosk.BlazorClient.Services
{
    public interface IOrderService
    {
        Task SubmitOrder(List<CartItem> cart);
    }

    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task SubmitOrder(List<CartItem> cart)
        {
            OrderDto createOrderDto = new OrderDto()
            {
                CreatedAt = DateTime.Now,
                Status = OrderStatusEnumDto.Submitted,
                Details = new List<OrderDetailDto>()
            };
            foreach (var item in cart)
            {
                var orderDetailDto = new OrderDetailDto()
                {
                    ProductId = item.Hotel.Id,
                    ProductName = item.Hotel.Name,
                    ProductPrice = item.Hotel.UnitPrice,
                    ImageUrl = item.Hotel.ImageUrl,
                    Count = item.Count,
                };

                createOrderDto.Details.Add(orderDetailDto);
                createOrderDto.OrderTotal += orderDetailDto.ProductPrice * orderDetailDto.Count;
            }


            var content = JsonConvert.SerializeObject(createOrderDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("order", bodyContent);
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
