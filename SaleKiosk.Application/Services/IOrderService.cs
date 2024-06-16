using SaleKiosk.SharedKernel.Dto;

namespace SaleKiosk.Application.Services
{
    public interface IOrderService
    {
        int Create(OrderDto dto);
        List<OrderDto> GetAll();
        OrderDto GetByIdWithDetails(int id);
    }
}
