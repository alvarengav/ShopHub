using OrderService.Application.Events;

namespace OrderService.Application.EventsHandler;

public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent @event)
    {
        Console.WriteLine($"Order created: {@event.OrderId}");
        return Task.CompletedTask;
    }
}
