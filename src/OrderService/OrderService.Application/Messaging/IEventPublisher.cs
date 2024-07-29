namespace OrderService.Application.Messaging;

public interface IEventPublisher
{
    void Publish<T>(T @event)
        where T : class;
}
