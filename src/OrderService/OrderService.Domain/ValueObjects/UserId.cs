namespace OrderService.Domain.ValueObjects;

public record UserId(string Value) : StronglyTypedId<string>(Value);
