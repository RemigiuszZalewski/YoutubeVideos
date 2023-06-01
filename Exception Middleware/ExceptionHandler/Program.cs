using ExceptionHandler.Exceptions;
using ExceptionHandler.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/player", () =>
{
    throw new PlayerNotFoundException("I was not found.", new Exception());
});

app.MapGet("/second", () =>
{
    throw new ArgumentException("Argument Exception Message", new Exception());
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();