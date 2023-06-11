using System.Linq;
using System.Text.Json;

namespace laba4db;

public static class DataCreator
{
    public static List<Table> GetTables(Dictionary<String, Dictionary<String, ColumnType>> config)
    {
        var tables = new List<Table>();

        foreach (var (name, tableConfig) in config)
        {
            tables.Add(
                new Table(tableConfig, DataCreator.GetData(new FileInfo(name), tableConfig), name)
            );
        }

        return tables;
    }

    public static Dictionary<String, Dictionary<String, ColumnType>> GetConfig(FileInfo schema)
    {
        using var reader = new StreamReader(schema.FullName);
        var json = reader.ReadToEnd();
        var strConfig = JsonSerializer.Deserialize<Dictionary<String, Dictionary<String, String>>>(json);

        var config = new Dictionary<String, Dictionary<String, ColumnType>>();
        foreach (var (tableName, tableConfig) in strConfig)
        {
            var newTableConfig = new Dictionary<String, ColumnType>();
            foreach (var (propertyName, propertyType) in tableConfig)
            {
                Enum.TryParse(propertyType, out ColumnType type);
                newTableConfig.Add(propertyName, type);
            }
            config.Add(tableName, newTableConfig);
        }

        return config;
    }

    public static Dictionary<String, Column> GetData(FileInfo csv, Dictionary<String, ColumnType> config)
    {
        var data = new Dictionary<String, Column>();
        var rawData = GetRawData(csv);

        var i = 0;
        foreach (var (name, type) in config)
        {
            if (!ValidateColumn(rawData.GetColumn(i), type, out var verifiedData))
            {
                Console.WriteLine($"Ошибка в {csv.Name}: элемент в {name} столбце не типа {type}");
                break;
            }

            data.Add(name, new Column(name, type, verifiedData));
            i++;
        }

        return data;
    }

    private static List<List<string>> GetRawData(FileInfo table)
    {
        var fileData = new List<List<string>>();

        var fileRows = File.ReadAllLines(table.FullName);
        foreach (var str in fileRows)
        {
            var list = new List<string>();
            var splitString = str.Split(",");

            foreach (var item in splitString)
            {
                list.Add(item);
            }

            fileData.Add(list);
        }

        return fileData;
    }

    private static bool ValidateColumn(List<string> data, ColumnType type, out List<object> validatedData)
    {
        bool isDataFullyConverted = true;
        validatedData = new List<object>();

        foreach (var str in data)
        {
            try
            {
                var element = str.Cast(type);
                if (element is not null)
                    validatedData.Add(element);
            }
            catch (Exception ex)
            {
                isDataFullyConverted = false;
            }
        }

        return isDataFullyConverted;
    }
}
