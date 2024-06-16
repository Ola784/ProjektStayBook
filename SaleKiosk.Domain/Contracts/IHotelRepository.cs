

using SaleKiosk.Domain.Models;

namespace SaleKiosk.Domain.Contracts
{
    // interfejsy repozytoriów specyficznych
    public interface IHotelRepository : IRepository<Hotel>
    {
        int GetMaxId();
        bool IsInUse(string email);
    }
}
