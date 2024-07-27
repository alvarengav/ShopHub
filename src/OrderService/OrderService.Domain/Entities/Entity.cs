namespace OrderService.Domain.Entities;

public class Entity<TId>
{
    public TId Id { get; protected set; }
}
