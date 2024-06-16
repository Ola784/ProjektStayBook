using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleKiosk.SharedKernel.Dto
{
    public class UpdateReviewDto
    {
        public int Id { get; set; }

        public string ReviewText { get; set; }
        public int Rate { get; set; }
    }
}
