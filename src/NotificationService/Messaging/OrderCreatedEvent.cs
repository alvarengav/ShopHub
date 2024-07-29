namespace NotificationService.Messaging;

public record OrderCreatedEvent(long OrderId, string UserId);
