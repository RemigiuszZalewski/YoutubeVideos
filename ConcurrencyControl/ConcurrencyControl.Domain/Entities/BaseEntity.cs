namespace ConcurrencyControl.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public byte[] RowVersion { get; set; }
}