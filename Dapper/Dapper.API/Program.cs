using Dapper.API.Modules;
using Dapper.Domain.Abstracts.Persistence;
using Dapper.Persistence.Options;
using Dapper.Persistence.Providers;
using Dapper.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DatabaseConnectionOptions>(
    builder.Configuration.GetSection(DatabaseConnectionOptions.DatabaseConnectionKey));

builder.Services.AddSingleton<ISqlConnectionProvider, SqlConnectionProvider>();
builder.Services.AddSingleton<IEntityAttributeValuesProvider, EntityAttributeValuesProvider>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.AddStudentEndpoints();

app.Run();