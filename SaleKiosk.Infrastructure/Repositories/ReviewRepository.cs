using Microsoft.EntityFrameworkCore;
using SaleKiosk.Domain.Contracts;
using SaleKiosk.Domain.Models;

namespace SaleKiosk.Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly KioskDbContext _kioskDbContext;

        public ReviewRepository(KioskDbContext context)
            : base(context)
        {
            _kioskDbContext = context;
        }

        public Review GetByIdWithDetails(int id)
        {
            var review = _kioskDbContext.Reviews
                .Include(o=>o.User)
                .Include(o=>o.Hotel)
                .Where(o => o.Id == id)
               .FirstOrDefault();

            return review;
        }

        public int GetMaxId()
        {
            return _kioskDbContext.Reviews.Max(x => x.Id);
        }

        public IQueryable<Review> GetAllWithDetails()
        {
            return _kioskDbContext.Reviews.Include(r => r.Hotel).Include(r => r.User);
        }

    }

}
