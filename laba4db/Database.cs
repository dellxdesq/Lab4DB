using laba4db;
using System.Text.Json;

namespace laba4db;

public class Database
{
    public List<Table> Tables { get; set; }
    private Dictionary<String, Dictionary<String, ColumnType>> Config;

    public Database(FileInfo schema)
    {
        Config = GetConfig(schema);
        Tables = new List<Table>();

        foreach (var (name, tableConfig) in Config)
        {
            Tables.Add(new Table(tableConfig, new FileInfo(name), name));
        }
    }

    private Dictionary<String, Dictionary<String, ColumnType>> GetConfig(FileInfo schema)
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

    public void Print()
    {
        foreach (var table in Tables)
        {
            table.Print();
            Console.WriteLine("\n*********************\n");
        }
    }
}
