using ConcurrencyControl.API.Modules;
using ConcurrencyControl.Domain.Abstracts.Persistence.Repositories;
using ConcurrencyControl.Persistence;
using ConcurrencyControl.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DemoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddBankAccountEndpoints();
app.UseHttpsRedirection();

app.Run();