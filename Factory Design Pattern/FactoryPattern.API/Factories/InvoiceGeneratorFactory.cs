using FactoryPattern.API.Abstracts;
using FactoryPattern.API.Enums;
using FactoryPattern.API.Generators;

namespace FactoryPattern.API.Factories;

public class InvoiceGeneratorFactory : IInvoiceGeneratorFactory
{
    private readonly IServiceProvider _serviceProvider;

    public InvoiceGeneratorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public IInvoiceGenerator CreateInvoiceGenerator(InvoiceFormat invoiceFormat)
    {
        return invoiceFormat switch
        {
            InvoiceFormat.Pdf => _serviceProvider.GetRequiredService<PdfInvoiceGenerator>(),
            InvoiceFormat.Txt => _serviceProvider.GetRequiredService<TxtInvoiceGenerator>(),
            InvoiceFormat.Csv => _serviceProvider.GetRequiredService<CsvInvoiceGenerator>(),
            _ => throw new ArgumentException("Invalid/Unsupported InvoiceFormat", nameof(invoiceFormat))
        };
    }
}