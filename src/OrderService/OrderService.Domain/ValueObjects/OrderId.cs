namespace OrderService.Domain.ValueObjects;

public record OrderId(long Value) : StronglyTypedId<long>(Value)
{
    public static implicit operator OrderId(long value) => new OrderId(value);
}
