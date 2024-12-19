using System.Text;
using FactoryPattern.API.Abstracts;
using FactoryPattern.API.Helpers;

namespace FactoryPattern.API.Generators;

public class CsvInvoiceGenerator : IInvoiceGenerator
{
    public byte[] GenerateInvoice(Guid invoiceId)
    {
        var invoice = MockInvoiceProvider.CreateMockInvoice(invoiceId);
        
        string csvContent = "Invoice ID,Date,Customer,Amount\n" +
                            $"{invoice.Id},{invoice.Date:yyyy-MM-dd},{invoice.CustomerName},{invoice.Amount:F2}";

        return Encoding.UTF8.GetBytes(csvContent);
    }

    public string GetContentType() => "text/csv";
}