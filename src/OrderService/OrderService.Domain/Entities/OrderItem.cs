using OrderService.Domain.ValueObjects;

namespace OrderService.Domain.Entities
{
    public class OrderItem : Entity<OrderItemId>
    {
        public OrderId OrderId { get; private set; }
        public ProductId ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        public OrderItem(
            OrderId orderId,
            ProductId productId,
            string productName,
            decimal unitPrice,
            int quantity
        )
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        private OrderItem() { }

        public void UpdateOrderItemDetails(
            ProductId productId,
            string productName,
            decimal unitPrice,
            int quantity
        )
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public decimal GetTotalPrice() => UnitPrice * Quantity;
    }
}
