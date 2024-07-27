using MediatR;
using OrderService.Application.Commands;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Handlers;

public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(
        UpdateOrderItemCommand request,
        CancellationToken cancellationToken
    )
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);

        if (order is null)
            throw new KeyNotFoundException($"Order with Id {request.OrderId} not found");

        var itemToUpdate = await _orderRepository.GetOrderItemAsync(request.OrderItemId);

        if (itemToUpdate is null)
            throw new KeyNotFoundException(
                $"OrderItem with Id ${request.OrderItemId} not found in Order {request.OrderId}"
            );

        itemToUpdate.UpdateOrderItemDetails(
            request.ProductId,
            request.ProductName,
            request.UnitPrice,
            request.Quantity
        );

        await _orderRepository.UpdateOrderAsync(order);

        return Unit.Value;
    }
}
