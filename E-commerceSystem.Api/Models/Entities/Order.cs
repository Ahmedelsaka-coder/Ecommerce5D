namespace E_commerceSystem.Api.Models.Entities
{

        public class Order
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalAmount { get; set; }
            public ICollection<OrderItem> OrderItems { get; set; }
        }

    
}
