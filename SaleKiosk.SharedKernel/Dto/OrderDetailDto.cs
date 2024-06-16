namespace SaleKiosk.SharedKernel.Dto
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ImageUrl { get; set; }
    }



}
