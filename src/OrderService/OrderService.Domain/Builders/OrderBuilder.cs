using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Domain.Builders;

public class OrderBuilder
{
    private UserId _userId;
    private readonly List<OrderItem> _orderItems = new();

    public OrderBuilder SetUserId(UserId userId)
    {
        _userId = userId;
        return this;
    }

    public OrderBuilder AddOrderItem(
        ProductId productId,
        string productName,
        decimal unitPrice,
        int quantity
    )
    {
        var orderItem = new OrderItem(null, productId, productName, unitPrice, quantity);
        _orderItems.Add(orderItem);
        return this;
    }

    public Order Build()
    {
        var order = new Order(_userId);
        foreach (var orderItem in _orderItems)
        {
            order.AddOrderItem(orderItem);
        }

        return order;
    }
}
