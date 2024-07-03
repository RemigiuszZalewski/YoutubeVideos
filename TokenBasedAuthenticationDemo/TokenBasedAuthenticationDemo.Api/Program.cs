using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TokenBasedAuthenticationDemo.Domain;
using TokenBasedAuthenticationDemo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter proper JWT token",
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Type = SecuritySchemeType.Http
    });
    
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "BearerAuth"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policyBuilder =>
    {
        policyBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
    });
});

builder.Services.AddDbContext<DemoDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"));
});

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<DemoDbContext>();

builder.Services.ConfigureAll<BearerTokenOptions>(option =>
{
    option.BearerTokenExpiration = TimeSpan.FromMinutes(1);
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.MapIdentityApi<IdentityUser>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/get-products", () => new List<Product>
{
    new()
    {
        Id = 1,
        Name = "Smartphone",
        Description = "A powerful smartphone with advanced features",
        Price = 699.99m,
        Quantity = 100
    },
    new()
    {
        Id = 2,
        Name = "Laptop",
        Description = "Thin and lightweight laptop with high performance",
        Price = 1299.99m,
        Quantity = 50
    },
    new()
    {
        Id = 3,
        Name = "Headphones",
        Description = "Wireless headphones with noise-canceling technology",
        Price = 199.99m,
        Quantity = 75
    },
    new()
    {
        Id = 4,
        Name = "Smart Watch",
        Description = "Fitness tracker with heart rate monitoring and GPS",
        Price = 149.99m,
        Quantity = 60
    },
    new()
    {
        Id = 5,
        Name = "Tablet",
        Description = "10-inch tablet with a high-resolution display",
        Price = 299.99m,
        Quantity = 40
    }
}).RequireAuthorization();

app.MapGet("/validate-access-token",
    () => Results.Ok(true)).RequireAuthorization();

app.Run();