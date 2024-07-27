using MediatR;
using OrderService.Application.Commands;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderId>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderId> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken
    )
    {
        var order = new Order(new UserId(request.userId));
        await _orderRepository.AddOrderAsync(order);

        foreach (var item in request.orderItems)
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
        return order.Id;
    }
}
