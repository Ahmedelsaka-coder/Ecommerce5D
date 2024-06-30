namespace E_commerceSystem.Api.Models
{
    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
