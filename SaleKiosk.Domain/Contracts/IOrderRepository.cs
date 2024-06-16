

using SaleKiosk.Domain.Models;

namespace SaleKiosk.Domain.Contracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        int GetMaxId();
        Order GetByIdWithDetails(int id);
    }
}
