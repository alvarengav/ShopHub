using MediatR;
using OrderService.Application.Commands;
using OrderService.Domain.Repositories;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Handlers;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsync(new OrderId(request.OrderId));

        if (order is null)
            throw new KeyNotFoundException($"Order with Id {request.OrderId} not found.");

        order.UpdateUserId(new UserId(request.UserId));

        await _orderRepository.LoadOrderItemsAsync(order);

        order.ClearOrderItems();
        foreach (var item in request.OrderItems)
        {
            order.AddOrderItem(
                order.Id,
                new ProductId(item.ProductId),
                item.ProductName,
                item.UnitPrice,
                item.Quantity
            );
        }

        await _orderRepository.UpdateOrderAsync(order);
        return Unit.Value;
    }
}
