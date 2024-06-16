using SaleKiosk.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleKiosk.Domain.Contracts
{
    public interface IReviewRepository : IRepository<Review>
    {
        int GetMaxId();
        Review GetByIdWithDetails(int id);

        IQueryable<Review>  GetAllWithDetails();
    }
}
