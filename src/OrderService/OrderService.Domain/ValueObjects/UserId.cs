namespace OrderService.Domain.ValueObjects;

public record UserId(long userId) : StronglyTypedId<long>(userId);
