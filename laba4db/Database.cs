using laba4db;
namespace laba4db;

public class Database
{
    public List<Table> Tables { get; set; }
    public Dictionary<String, Dictionary<String, ColumnType>> Config;

    public Database(Dictionary<String, Dictionary<String, ColumnType>> config, List<Table> tables)
    {
        Tables = tables;
        Config = config;
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
