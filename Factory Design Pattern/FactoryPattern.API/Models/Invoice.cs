namespace FactoryPattern.API.Models;

public class Invoice
{
    public Guid Id { get; set; }
    public required DateTime Date { get; set; }
    public required string CustomerName { get; set; }
    public required decimal Amount { get; set; }
}