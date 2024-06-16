using SaleKiosk.SharedKernel.Dto;
using System.ComponentModel.DataAnnotations;

namespace SaleKiosk.BlazorClient.ViewModels
{
    public class CartItem
    {
        public HotelDto Hotel { get; set; }
        public int Count { get; set; }
    }
}
