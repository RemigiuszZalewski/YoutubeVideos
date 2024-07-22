using System.Data.SqlClient;

namespace Dapper.Domain.Abstracts.Persistence;

public interface ISqlConnectionProvider
{
    SqlConnection GetSqlConnection();
}