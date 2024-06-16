namespace SaleKiosk.Domain.Contracts
{
    public interface IKioskUnitOfWork : IDisposable
    {
        IHotelRepository HotelRepository { get; }
        IOrderRepository OrderRepository { get; }
        IUserRepository UserRepository { get; }

        IReviewRepository ReviewRepository { get; }

        void Commit();
    }
}
