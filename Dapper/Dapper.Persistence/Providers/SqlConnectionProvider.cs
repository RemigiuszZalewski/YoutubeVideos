using System.Data.SqlClient;
using Dapper.Domain.Abstracts.Persistence;
using Dapper.Persistence.Options;
using Microsoft.Extensions.Options;

namespace Dapper.Persistence.Providers;

public class SqlConnectionProvider : ISqlConnectionProvider
{
    private readonly DatabaseConnectionOptions _options;

    public SqlConnectionProvider(IOptions<DatabaseConnectionOptions> options)
    {
        _options = options.Value;
    }

    public SqlConnection GetSqlConnection()
    {
        return new SqlConnection(_options.DbConnectionString);
    }
}