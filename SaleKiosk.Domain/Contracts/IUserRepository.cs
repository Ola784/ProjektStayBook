using SaleKiosk.Domain.Models;


namespace SaleKiosk.Domain.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        int GetMaxId();
        User GetByIdWithDetails(int id);

        bool IsInUse(string email);
    }
}
