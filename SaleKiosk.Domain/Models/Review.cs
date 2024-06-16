using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleKiosk.Domain.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int HotelId { get; set; } // Powiązanie z konkretnym hotelem
        public int UserId { get; set; } // Powiązanie z konkretnym użytkownikiem
        public string ReviewText { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }
        public Hotel Hotel { get; set; }
        public User User { get; set; }
    }
}
