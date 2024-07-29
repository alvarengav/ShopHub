namespace OrderService.Application.EventsHandler;

internal interface IEventHandler<in TEvent>
    where TEvent : class
{
    Task Handle(TEvent @event);
}
