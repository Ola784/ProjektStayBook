using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleKiosk.SharedKernel.Dto
{
    public class CreateReviewDto
    {
        public int HotelId { get; set; }
        public int UserId { get; set; }
        public string ReviewText { get; set; }
        public int Rate { get; set; }

      //  public string HotelName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
