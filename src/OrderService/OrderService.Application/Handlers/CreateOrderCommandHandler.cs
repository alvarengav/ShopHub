using MediatR;
using OrderService.Application.Commands;
using OrderService.Application.Events;
using OrderService.Application.Messaging;
using OrderService.Domain.Builders;
using OrderService.Domain.Repositories;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderId>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEventPublisher _eventPublisher;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IEventPublisher eventPublisher
    )
    {
        _orderRepository = orderRepository;
        _eventPublisher = eventPublisher;
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

        _eventPublisher.Publish(
            new OrderCreatedEvent(order.Id.Value, order.UserId.Value.ToString())
        );

        return order.Id;
    }
}
