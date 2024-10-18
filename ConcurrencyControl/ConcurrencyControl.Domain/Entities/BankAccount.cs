namespace ConcurrencyControl.Domain.Entities;

public class BankAccount : BaseEntity
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public string OwnerName { get; set; }
}