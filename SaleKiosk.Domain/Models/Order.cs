using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleKiosk.Domain.Models
{
    public enum OrderStatusEnum
    {
        Submitted,
        Completed,
    }

    public class Order
    {
        public int Id { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatusEnum Status { get; set; }
        public List<OrderDetail> Details { get; set; } = new List<OrderDetail>();
    }
}
