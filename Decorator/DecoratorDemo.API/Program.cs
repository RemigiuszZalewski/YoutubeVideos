using DecoratorDemo.API;
using DecoratorDemo.API.Abstracts;
using DecoratorDemo.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.Decorate<IStudentService, PerformanceLoggingStudentService>();

builder.Services.AddDbContext<DecoratorDemoDbContext>(
    opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DecoratorDemoDbContext>();
    
    if (!await context.Students.AnyAsync())
    {
        await context.SeedStudentsAsync();
    }
}

app.MapGet("/students", async (IStudentService studentService) 
    => Results.Ok(await studentService.GetStudentsAsync()));

app.UseHttpsRedirection();
app.Run();