using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Domain.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrderAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetAllOrdersWithItemsAsync(
        CancellationToken cancellationToken = default
    );
    Task<Order> GetOrderByIdAsync(OrderId id, CancellationToken cancellationToken = default);
    Task<Order> GetOrderWithItemsByIdAsync(
        OrderId id,
        CancellationToken cancellationToken = default
    );
    Task AddOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task UpdateOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task DeleteOrderAsync(OrderId id, CancellationToken cancellationToken = default);
    Task LoadOrderItemsAsync(Order order, CancellationToken cancellationToken = default);
    Task<OrderItem> GetOrderItemAsync(
        OrderItemId OrderItemId,
        CancellationToken cancellationToken = default
    );
    Task<OrderItem> GetOrderItemByIdAsync(
        OrderItemId orderItemId,
        CancellationToken cancellationToken = default
    );
}
