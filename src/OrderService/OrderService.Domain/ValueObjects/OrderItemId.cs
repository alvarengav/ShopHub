namespace OrderService.Domain.ValueObjects;

public record OrderItemId(long Value) : StronglyTypedId<long>(Value)
{
    public static implicit operator OrderItemId(long Value) => new OrderItemId(Value);
}
