using OrderService.Domain.ValueObjects;

namespace OrderService.Domain.Entities;

public class Order : Entity<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public UserId UserId { get; private set; }

    public DateTime OrderDate { get; private set; }

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    private Order() { }

    public Order(UserId userId)
    {
        UserId = userId;
        OrderDate = DateTime.UtcNow;
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
        _orderItems.Remove(orderItem);
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
        _orderItems.Add(orderItem);
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        _orderItems.Add(orderItem);
    }

    public void ClearOrderItems()
    {
        _orderItems.Clear();
    }
}
