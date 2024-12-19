namespace FactoryPattern.API.Abstracts;

public interface IInvoiceGenerator
{
    byte[] GenerateInvoice(Guid invoiceId);
    string GetContentType();
}