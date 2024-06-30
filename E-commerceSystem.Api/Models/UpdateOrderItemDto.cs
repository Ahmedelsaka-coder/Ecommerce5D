namespace E_commerceSystem.Api.Models
{
    public class UpdateOrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
