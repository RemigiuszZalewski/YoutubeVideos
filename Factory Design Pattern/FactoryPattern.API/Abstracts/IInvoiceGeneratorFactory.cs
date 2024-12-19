using FactoryPattern.API.Enums;

namespace FactoryPattern.API.Abstracts;

public interface IInvoiceGeneratorFactory
{
    IInvoiceGenerator CreateInvoiceGenerator(InvoiceFormat invoiceFormat);
}