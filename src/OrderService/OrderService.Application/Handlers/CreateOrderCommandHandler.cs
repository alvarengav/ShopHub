using MediatR;
using OrderService.Application.Commands;
using OrderService.Domain.Builders;
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
        var orderBuilder = new OrderBuilder().SetUserId(new UserId(request.userId));

        foreach (var item in request.orderItems)
        {
            orderBuilder.AddOrderItem(
                new ProductId(item.ProductId),
                item.ProductName,
                item.UnitPrice,
                item.Quantity
            );
        }

        var order = orderBuilder.Build();
        await _orderRepository.AddOrderAsync(order);
        return order.Id;
    }
}
