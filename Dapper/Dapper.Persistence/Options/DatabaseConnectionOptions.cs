namespace Dapper.Persistence.Options;

public class DatabaseConnectionOptions
{
    public const string DatabaseConnectionKey = "ConnectionStrings";

    public string DbConnectionString { get; set; } = string.Empty;
}