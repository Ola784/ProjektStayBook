using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleKiosk.Domain.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; } = "/images/no-image-icon.png";
        public List<Review> Reviews { get; set; } = new List<Review>();

    }

}
