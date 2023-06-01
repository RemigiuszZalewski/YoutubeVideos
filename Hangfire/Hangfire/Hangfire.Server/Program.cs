using Hangfire;
using Hangfire.Producer;
using Hangfire.Server.Jobs;
using Hangfire.Server.Options;
using Hangfire.Shared.Jobs;
using Hangfire.Shared.Services;

var host = Host.CreateDefaultBuilder(args);
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

host.ConfigureServices(services =>
{
    services.AddHangfire(opt =>
    {
        opt.UseSqlServerStorage(configuration.GetConnectionString("DbConnectionString"))
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings();
    });

    services.AddHangfireServer();
    services.AddScoped<ISendEmailJob, SendEmailJob>();
    services.AddScoped<IEmailService, EmailService>();
    services.Configure<ServerOptions>(configuration.GetSection(ServerOptions.ServerOptionsKey));
});
    
host.Build().Run();