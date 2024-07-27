namespace OrderService.Domain.ValueObjects;

public abstract record StronglyTypedId<T>(T Value)
    where T : struct, IEquatable<T>
{
    public override string ToString() => Value.ToString() ?? "";

    public static implicit operator T(StronglyTypedId<T> StronglyTypeId) => StronglyTypeId.Value;
}
