namespace Dapper.Domain.Abstracts.Persistence;

public interface IEntityAttributeValuesProvider
{
    string GetTableName<T>();
    Dictionary<string, string> GetColumnsAndModelPropertyNames<T>(bool addKey = true);
    string GetFormattedStringFromColumnsAndPropertyNames<T>(Dictionary<string, string> columnNamePropertyNames,
        string formatString);

    (string?, string) GetKeyColumnNamePropertyName<T>();
}