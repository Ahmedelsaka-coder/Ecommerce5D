namespace E_commerceSystem.Api.Models
{
    public class UpdateOrderDto
    {
        public decimal TotalAmount { get; set; }
        public List<UpdateOrderItemDto> OrderItems { get; set; }
    }
}
