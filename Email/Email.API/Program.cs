using Email.API.Abstracts;
using Email.API.Contracts;
using Email.API.Options;
using Email.API.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<GmailOptions>(
    builder.Configuration.GetSection(GmailOptions.GmailOptionsKey));

builder.Services.AddScoped<IMailService, GmailService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/email", async ([FromBody] SendEmailRequest sendEmailRequest,
    IMailService mailService) =>
{
    await mailService.SendEmailAsync(sendEmailRequest);
    return Results.Ok("Email sent successfully");
});

app.Run();