namespace Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; } = Guid.NewGuid();
}