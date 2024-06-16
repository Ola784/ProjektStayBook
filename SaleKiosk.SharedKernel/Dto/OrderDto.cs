namespace SaleKiosk.SharedKernel.Dto
{
    public enum OrderStatusEnumDto
    {
        Submitted,
        Completed,
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatusEnumDto Status { get; set; }
        public List<OrderDetailDto> Details { get; set; } = new List<OrderDetailDto>();
    }
}
