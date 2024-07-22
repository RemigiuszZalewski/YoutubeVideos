using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Dapper.Domain.Abstracts.Persistence;

namespace Dapper.Persistence.Providers;

public class EntityAttributeValuesProvider : IEntityAttributeValuesProvider
{
    public string GetTableName<T>()
    {
        var attributes = typeof(T).GetCustomAttributes(typeof(TableAttribute), true);

        if (attributes.Length > 0)
        {
            var tableAttr = (TableAttribute)attributes[0];
            return tableAttr.Name;
        }

        throw new Exception("Table attribute is missing.");
    }

    public Dictionary<string, string> GetColumnsAndModelPropertyNames<T>(bool addKey = true)
    {
        PropertyInfo[] propertyInfos = typeof(T).GetProperties();
        var columnNamePropertyNameDict = new Dictionary<string, string>();

        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            if (propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() is
                ColumnAttribute columnAttribute)
            {
                if (addKey || propertyInfo.GetCustomAttributes(typeof(KeyAttribute), true).Length == 0)
                {
                    if (columnAttribute.Name != null)
                    {
                        columnNamePropertyNameDict.Add(columnAttribute.Name, propertyInfo.Name);
                    }
                }
            }
            else
            {
                throw new Exception($"Column Attribute is missing for property: {propertyInfo.Name}");
            }
        }

        return columnNamePropertyNameDict;
    }

    public string GetFormattedStringFromColumnsAndPropertyNames<T>(Dictionary<string, string> columnNamePropertyNames, string formatString)
    {
        var result = string.Join(", ", columnNamePropertyNames
            .Select(columnNamePropName =>
                string.Format(formatString, columnNamePropName.Key, columnNamePropName.Value)));

        return result;
    }

    public (string?, string) GetKeyColumnNamePropertyName<T>()
    {
        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
        {
            if (propertyInfo.GetCustomAttributes(typeof(KeyAttribute), true).Length > 0 &&
                propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() is
                    ColumnAttribute columnAttribute)
            {
                return (columnAttribute.Name, propertyInfo.Name);
            }
        }

        throw new Exception("Provided model does not contain KeyColumnAttribute");
    }
}