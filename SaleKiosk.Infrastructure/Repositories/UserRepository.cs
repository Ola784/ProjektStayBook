using SaleKiosk.Domain.Contracts;
using SaleKiosk.Domain.Models;


namespace SaleKiosk.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly KioskDbContext _kioskDbContext;

        public UserRepository(KioskDbContext context)
            : base(context)
        {
            _kioskDbContext = context;
        }

        public User GetByIdWithDetails(int id)
        {
            var user = _kioskDbContext.Users
                .Where(o => o.Id == id)
               .FirstOrDefault();

            return user;
        }

        public int GetMaxId()
        {
            return _kioskDbContext.Users.Max(x => x.Id);
        }

        public bool IsInUse(string username)
        {
            var result = _kioskDbContext.Users.Any(x => x.Username == username);
            return result;
        }

      
    }
}
