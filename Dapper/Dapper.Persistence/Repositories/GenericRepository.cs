using Dapper.Domain.Abstracts.Persistence;

namespace Dapper.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ISqlConnectionProvider _sqlConnectionProvider;
    private readonly IEntityAttributeValuesProvider _entityAttributeValuesProvider;

    public GenericRepository(ISqlConnectionProvider sqlConnectionProvider, IEntityAttributeValuesProvider entityAttributeValuesProvider)
    {
        _sqlConnectionProvider = sqlConnectionProvider;
        _entityAttributeValuesProvider = entityAttributeValuesProvider;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        await using var sqlConnection = _sqlConnectionProvider.GetSqlConnection();
        
        string tableName = _entityAttributeValuesProvider.GetTableName<T>();
        Dictionary<string, string> columnNamePropertyNameDict =
            _entityAttributeValuesProvider.GetColumnsAndModelPropertyNames<T>();

        string aliasedColumnNamesPropNames =
            _entityAttributeValuesProvider.GetFormattedStringFromColumnsAndPropertyNames<T>
                (columnNamePropertyNameDict, "{0} AS {1}");

        var sql = $"SELECT {aliasedColumnNamesPropNames} FROM {tableName}";

        return await sqlConnection.QueryAsync<T>(sql);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        await using var sqlConnection = _sqlConnectionProvider.GetSqlConnection();
        
        string tableName = _entityAttributeValuesProvider.GetTableName<T>();
        
        Dictionary<string, string> columnNamePropertyNameDict =
            _entityAttributeValuesProvider.GetColumnsAndModelPropertyNames<T>();

        string aliasedColumnNamesPropNames =
            _entityAttributeValuesProvider.GetFormattedStringFromColumnsAndPropertyNames<T>
                (columnNamePropertyNameDict, "{0} AS {1}");
        
        (string? columnName, string propertyName) keyColumnNamePropertyName = _entityAttributeValuesProvider.
            GetKeyColumnNamePropertyName<T>();

        var keyColumnName = keyColumnNamePropertyName.columnName;
        var keyPropertyName = keyColumnNamePropertyName.propertyName;

        var sql = $"SELECT {aliasedColumnNamesPropNames} FROM {tableName} WHERE {keyColumnName} = @{keyPropertyName}";

        var parameters = new DynamicParameters();
        parameters.Add($"@{keyPropertyName}", id);

        return await sqlConnection.QueryFirstOrDefaultAsync<T>(sql, parameters);
    }

    public async Task<bool> AddAsync(T entity)
    {
        await using var sqlConnection = _sqlConnectionProvider.GetSqlConnection();
        
        string tableName = _entityAttributeValuesProvider.GetTableName<T>();
        
        Dictionary<string, string> columnNamePropertyNameDict =
            _entityAttributeValuesProvider.GetColumnsAndModelPropertyNames<T>(addKey: false);

        var sql = $"INSERT INTO {tableName} ({string.Join(", ", columnNamePropertyNameDict.Keys)}) " +
                  $"VALUES ({string.Join(", ",
                      columnNamePropertyNameDict.Values.Select(propName => $"@{propName}"))})";

        int rowsAffected = await sqlConnection.ExecuteAsync(sql, entity);

        return rowsAffected > 0;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        await using var sqlConnection = _sqlConnectionProvider.GetSqlConnection();
        
        string tableName = _entityAttributeValuesProvider.GetTableName<T>();
        
        Dictionary<string, string> columnNamePropertyNameDict =
            _entityAttributeValuesProvider.GetColumnsAndModelPropertyNames<T>(addKey: false);
        
        (string? keyColumnName, string keyPropertyName) keyColumnNamePropertyName = _entityAttributeValuesProvider.
            GetKeyColumnNamePropertyName<T>();
        
        string aliasedColumnNamesPropNames =
            _entityAttributeValuesProvider.GetFormattedStringFromColumnsAndPropertyNames<T>
                (columnNamePropertyNameDict, "{0} = @{1}");

        var sql = $"UPDATE {tableName} SET {aliasedColumnNamesPropNames}" +
                  $" WHERE {keyColumnNamePropertyName.keyColumnName} = @{keyColumnNamePropertyName.keyPropertyName}";

        int rowsAffected = await sqlConnection.ExecuteAsync(sql, entity);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var sqlConnection = _sqlConnectionProvider.GetSqlConnection();
        
        string tableName = _entityAttributeValuesProvider.GetTableName<T>();
        (string?, string) keyColumnNamePropertyName = _entityAttributeValuesProvider.
            GetKeyColumnNamePropertyName<T>();

        var keyColumnName = keyColumnNamePropertyName.Item1;
        var keyPropertyName = keyColumnNamePropertyName.Item2;

        var sql = $"DELETE FROM {tableName} WHERE {keyColumnName} = @{keyPropertyName};";

        var parameters = new DynamicParameters();
        parameters.Add($"@{keyPropertyName}", id);

        return await sqlConnection.ExecuteAsync(sql, parameters) > 0;
    }
}