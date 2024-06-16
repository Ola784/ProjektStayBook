using SaleKiosk.Domain.Contracts;

namespace SaleKiosk.Infrastructure
{
    // implementacja Unit of Work
    public class KioskUnitOfWork : IKioskUnitOfWork
    {
        private readonly KioskDbContext _context;

        public IHotelRepository HotelRepository { get; }
        public IOrderRepository OrderRepository { get; }

        public IUserRepository UserRepository { get; }
        public IReviewRepository ReviewRepository { get; }


        public KioskUnitOfWork(KioskDbContext context, IHotelRepository hotelRepository, IOrderRepository orderRepository, IUserRepository userRepository, IReviewRepository reviewRepository)
        {
            this._context = context;
            this.HotelRepository = hotelRepository;
            this.OrderRepository = orderRepository;
            this.UserRepository = userRepository;
            this.ReviewRepository = reviewRepository;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
