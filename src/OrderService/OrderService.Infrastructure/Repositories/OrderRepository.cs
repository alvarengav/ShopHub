using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using OrderService.Domain.ValueObjects;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllOrderAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await _context.Orders.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Order> GetOrderByIdAsync(OrderId id, CancellationToken cancellationToken)
    {
        return await _context.Orders.FirstOrDefaultAsync(
            order => order.Id == id,
            cancellationToken
        );
    }

    public async Task<IEnumerable<Order>> GetAllOrdersWithItemsAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await _context
            .Orders.Include(order => order.OrderItems)
            .ToListAsync(cancellationToken);
    }

    public async Task AddOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteOrderAsync(OrderId id, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is not null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<Order> GetOrderWithItemsByIdAsync(
        OrderId id,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Orders.Include(order => order.OrderItems)
            .FirstOrDefaultAsync(order => order.Id == id, cancellationToken);
    }

    public async Task LoadOrderItemsAsync(
        Order order,
        CancellationToken cancellationToken = default
    )
    {
        await _context
            .Entry(order)
            .Collection(order => order.OrderItems)
            .LoadAsync(cancellationToken);
    }

    public async Task<OrderItem> GetOrderItemAsync(
        OrderItemId orderItemId,
        CancellationToken cancellationToken = default
    )
    {
        return await _context.OrderItems.FirstOrDefaultAsync(oi => oi.Id == orderItemId);
    }

    public async Task<OrderItem> GetOrderItemByIdAsync(
        OrderItemId orderItemId,
        CancellationToken cancellationToken = default
    )
    {
        return await _context.OrderItems.FirstOrDefaultAsync(item => item.Id == orderItemId);
    }
}
