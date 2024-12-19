using System.Text;
using FactoryPattern.API.Abstracts;
using FactoryPattern.API.Helpers;

namespace FactoryPattern.API.Generators;

public class TxtInvoiceGenerator : IInvoiceGenerator
{
    public byte[] GenerateInvoice(Guid invoiceId)
    {
        var invoice = MockInvoiceProvider.CreateMockInvoice(invoiceId);
        
        string content = $"Invoice #{invoice.Id}\n" +
                         $"Date: {invoice.Date:yyyy-MM-dd}\n" +
                         $"Customer: {invoice.CustomerName}\n" +
                         $"Amount: ${invoice.Amount:F2}";

        return Encoding.UTF8.GetBytes(content);
    }

    public string GetContentType() => "text/plain";
}