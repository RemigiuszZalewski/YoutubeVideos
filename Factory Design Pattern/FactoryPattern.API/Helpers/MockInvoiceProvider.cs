using FactoryPattern.API.Models;

namespace FactoryPattern.API.Helpers;

public class MockInvoiceProvider
{
    public static Invoice CreateMockInvoice(Guid invoiceId)
    {
        return new Invoice
        {
            Id = invoiceId,
            Date = new DateTime(2024,12,12),
            CustomerName = "John Doe",
            Amount = 299.99m
        };
    }
}