using MediatR;
using OrderService.Application.Commands;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Handlers;

public class RemoveOrderItemCommandHandler : IRequestHandler<RemoveOrderItemCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;

    public RemoveOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(
        RemoveOrderItemCommand request,
        CancellationToken cancellationToken
    )
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with Id {request.OrderId} not found.");
        }

        var itemToRemove = await _orderRepository.GetOrderItemByIdAsync(request.OrderItemId);
        if (itemToRemove == null)
        {
            throw new KeyNotFoundException(
                $"Order item with Id {request.OrderItemId} not found in order {request.OrderId}."
            );
        }

        order.RemoveOrderItem(itemToRemove);
        await _orderRepository.UpdateOrderAsync(order);

        return Unit.Value;
    }
}
