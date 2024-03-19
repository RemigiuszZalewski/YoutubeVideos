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
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"));
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policyBuilder =>
    {
        policyBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5000");
    });
});

builder.Services.AddApplication();
builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var serviceScope = app.Services.CreateScope();
    using var dbContext = serviceScope.ServiceProvider.GetService<MoviesDbContext>();
    dbContext?.Database.Migrate();
}

app.UseExceptionHandler(_ => { });
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.AddMoviesEndpoints();
app.Run();

// "ConnectionStrings": {
//     "DbConnectionString": "Server=database.server,1433;Database=MoviesDb;User Id=SA;Password=A&VeryComplex123Password;MultiSubnetFailover=True;TrustServerCertificate=True;Encrypt=False;"
// }