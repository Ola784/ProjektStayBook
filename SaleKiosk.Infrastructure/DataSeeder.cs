using SaleKiosk.Domain.Models;

namespace SaleKiosk.Infrastructure
{
    public class DataSeeder
    {
        private readonly KioskDbContext _dbContext;

        public DataSeeder(KioskDbContext context)
        {
            this._dbContext = context;
        }

        public void Seed()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Hotels.Any())
                {
                    var hotels = new List<Hotel>
                    {
                        new Hotel()
                        {
                            Id = 1,
                            Name = "Marriott",
                            Description = "5 gwiazdkowy przjazny rodzinom z dziećmi",
                            UnitPrice = 60,
                            CreatedAt = DateTime.Now.AddDays(-3),
                            ImageUrl = "/images/hotel1.jpg",
                        },

                        new Hotel()
                        {
                            Id = 2,
                            Name = "Hilton",
                            Description = "4 gwiazdkowy dla miłośników zwierząt",
                            UnitPrice = 40,
                            CreatedAt = DateTime.Now.AddDays(-2),
                            ImageUrl = "/images/hotel2.jpg",
                        },
                    };

                    _dbContext.Hotels.AddRange(hotels);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Users.Any())
                {
                    var users = new List<User>
                    {
                        new User()
                        {
                            Id = 1,
                            FirstName="Jan",
                            LastName="Kowalski",
                            Email="jan.kowalski@onet.pl",
                            PhoneNumber="123456",
                            Username="Jan123",
                            ImageUrl = "/images/image2.jpg",
                        },

                        new User()
                        {
                            Id = 2,
                            FirstName="Ania",
                            LastName="Nowak",
                            Email="anna.nowak@onet.pl",
                            PhoneNumber="345678",
                            Username="Ania11",
                            ImageUrl = "/images/image3.jpg",
                        },
                    };

                    _dbContext.Users.AddRange(users);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Orders.Any())
                {
                    var orders = new List<Order>
                    {
                        new Order()
                        {
                            Id = 1,
                            OrderTotal = 120,
                            CreatedAt = DateTime.Now.AddDays(-1),
                            Status = OrderStatusEnum.Completed,
                            Details = new List<OrderDetail>
                            {
                                new OrderDetail
                                {
                                    Id = 1,
                                    Count = 2,
                                    ProductId = 1, // Marriott
                                    ProductName = "Marriott",
                                    ProductPrice = 60,
                                    ImageUrl = "/images/hotel1.jpg",
                                }
                            }
                        }
                    };

                    _dbContext.Orders.AddRange(orders);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Reviews.Any())
                {
                    var reviews = new List<Review>
                    {
                         new Review()
                         {
                            Id = 1,
                            HotelId = 1,
                            UserId=1,
                            Rate = 3,
                            ReviewText = "Fantastyczny hotel, bardzo polecam!",
                            CreatedAt = DateTime.Now
                         }
                    };

                    _dbContext.Reviews.AddRange(reviews);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}














