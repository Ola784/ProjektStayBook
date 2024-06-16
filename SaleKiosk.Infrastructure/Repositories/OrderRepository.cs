using Microsoft.EntityFrameworkCore;
using SaleKiosk.Domain.Contracts;
using SaleKiosk.Domain.Models;

namespace SaleKiosk.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly KioskDbContext _kioskDbContext;

        public OrderRepository(KioskDbContext context)
            : base(context)
        {
            _kioskDbContext = context;
        }

        public int GetMaxId()
        {
            if (!_kioskDbContext.Orders.Any())
                return 1;

            return _kioskDbContext.Orders.Max(x => x.Id);
        }

        public Order GetByIdWithDetails(int id)
        {
            var order = _kioskDbContext.Orders
                .Include(o => o.Details)
                .Where(o => o.Id == id)
                .FirstOrDefault();

            return order;
        }
    }

}
