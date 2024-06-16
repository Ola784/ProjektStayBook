namespace SaleKiosk.SharedKernel.Dto
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImageUrl { get; set; }
        public int Count { get; set; } = 1;
    }

}
