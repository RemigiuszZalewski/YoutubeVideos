using FactoryPattern.API.Abstracts;
using FactoryPattern.API.Helpers;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FactoryPattern.API.Generators;

public class PdfInvoiceGenerator : IInvoiceGenerator
{
    public byte[] GenerateInvoice(Guid invoiceId)
    {
        var invoice = MockInvoiceProvider.CreateMockInvoice(invoiceId);
        
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.Header().Text($"Invoice #{invoice.Id}").SemiBold().FontSize(20);
                page.Content().PaddingVertical(1, Unit.Centimetre).Column(column =>
                {
                    column.Item().Text($"Date: {invoice.Date:yyyy-MM-dd}");
                    column.Item().Text($"Customer: {invoice.CustomerName}");
                    column.Item().Text($"Amount: ${invoice.Amount:F2}").FontSize(14);
                });
                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Page ");
                    x.CurrentPageNumber();
                });
            });
        });

        return document.GeneratePdf();
    }

    public string GetContentType() => "application/pdf";
}