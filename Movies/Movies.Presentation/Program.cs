using Microsoft.EntityFrameworkCore;
using Movies.Application;
using Movies.Infrastructure;
using Movies.Presentation.Handlers;
using Movies.Presentation.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MoviesDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DbConnectionString"));
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policyBuilder =>
    {
        policyBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173");
    });
});

builder.Services.AddApplication();
builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.AddMoviesEndpoints();
app.Run();