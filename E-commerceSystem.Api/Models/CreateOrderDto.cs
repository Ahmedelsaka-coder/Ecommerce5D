namespace E_commerceSystem.Api.Models
{
    public class CreateOrderDto
    {
        public decimal TotalAmount { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
