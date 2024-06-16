using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleKiosk.Domain.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ImageUrl { get; set; } = "/images/no-image-icon-23505.png";
    }
}

