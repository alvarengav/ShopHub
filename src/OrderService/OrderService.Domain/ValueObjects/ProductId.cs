namespace OrderService.Domain.ValueObjects;

public record ProductId(long Value) : StronglyTypedId<long>(Value)
{
    public static implicit operator ProductId(long value) => new ProductId(value);
}
