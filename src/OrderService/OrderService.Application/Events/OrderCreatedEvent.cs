namespace OrderService.Application.Events;

public record OrderCreatedEvent(long OrderId, string UserId);
