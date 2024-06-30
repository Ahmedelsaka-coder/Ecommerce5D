using static E_commerceSystem.Api.Models.Entities.Order;

namespace E_commerceSystem.Api.Models.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }

    }
}
