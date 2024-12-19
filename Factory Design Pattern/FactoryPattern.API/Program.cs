using FactoryPattern.API.Abstracts;
using FactoryPattern.API.Enums;
using FactoryPattern.API.Factories;
using FactoryPattern.API.Generators;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IInvoiceGeneratorFactory, InvoiceGeneratorFactory>();
builder.Services.AddTransient<PdfInvoiceGenerator>();
builder.Services.AddTransient<TxtInvoiceGenerator>();
builder.Services.AddTransient<CsvInvoiceGenerator>();

builder.Services.AddOpenApi();
QuestPDF.Settings.License = LicenseType.Community;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/api/invoice/{id}/{format}", (Guid id, InvoiceFormat format,
        IInvoiceGeneratorFactory invoiceGeneratorFactory) =>
    {
        var generator = invoiceGeneratorFactory.CreateInvoiceGenerator(format);
        var invoiceData = generator.GenerateInvoice(id);
        var contentType = generator.GetContentType();

        string fileName = $"Invoice_{id}.{format.ToString().ToLower()}";

        return Results.File(invoiceData, contentType, fileName);
    })
    .WithName("GenerateInvoice")
    .WithOpenApi();

app.UseHttpsRedirection();

app.Run();