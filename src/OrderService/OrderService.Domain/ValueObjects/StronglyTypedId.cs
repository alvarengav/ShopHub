namespace OrderService.Domain.ValueObjects;

public abstract record StronglyTypedId<T>(T Value)
    where T : IEquatable<T>
{
    public override string ToString() => Value.ToString() ?? string.Empty;

    public static implicit operator T(StronglyTypedId<T> StronglyTypeId) => StronglyTypeId.Value;
}
