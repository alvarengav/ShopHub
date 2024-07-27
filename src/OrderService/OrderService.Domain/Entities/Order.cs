using OrderService.Domain.ValueObjects;

namespace OrderService.Domain.Entities;

public class Order : Entity<OrderId>
{
    public UserId UserId { get; private set; }

    public DateTime OrderDate { get; private set; }

    public ICollection<OrderItem> OrderItems { get; private set; }

    public Order(UserId userId)
    {
        UserId = userId;
        OrderDate = DateTime.UtcNow;
        OrderItems = new List<OrderItem>();
    }

    public void UpdateUserId(UserId userId) => UserId = userId;

    public void UpdateOrderItem(
        OrderItem orderItem,
        ProductId productId,
        string productName,
        decimal unitPrice,
        int quantity
    )
    {
        orderItem.UpdateOrderItemDetails(productId, productName, unitPrice, quantity);
    }

    public void RemoveOrderItem(OrderItem orderItem)
    {
        OrderItems.Remove(orderItem);
    }

    public void AddOrderItem(
        OrderId orderId,
        ProductId productId,
        string productName,
        decimal unitPrice,
        int quantity
    )
    {
        var orderItem = new OrderItem(orderId, productId, productName, unitPrice, quantity);
        OrderItems.Add(orderItem);
    }

    public void ClearOrderItems()
    {
        OrderItems.Clear();
    }
}
