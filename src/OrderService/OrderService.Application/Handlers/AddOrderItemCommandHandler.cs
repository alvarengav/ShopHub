using MediatR;
using OrderService.Application.Commands;
using OrderService.Domain.Repositories;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Handlers;

public class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;

    public AddOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);

        if (order is null)
            throw new KeyNotFoundException($"Order with Id {request.OrderId} not found");

        order.AddOrderItem(
            order.Id,
            new ProductId(request.ProductId),
            request.ProductName,
            request.UnitPrice,
            request.Quantity
        );

        await _orderRepository.UpdateOrderAsync(order);

        return Unit.Value;
    }
}
