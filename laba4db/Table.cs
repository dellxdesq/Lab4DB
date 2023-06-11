using System.Data.Common;
using System.Text.Json;

namespace laba4db;

public class Table
{
    private String Name { get; set; }
    public Dictionary<String, Column> Data { get; set; }
    public Dictionary<String, ColumnType> Config { get; set; }

    public int MaxWidth
    {
        get
        {
            var max = 0;
            foreach (var column in Data.Values)
            {
                max = Math.Max(column.MaxWidth, max);
            }

            return max;
        }
    }

    public Table(Dictionary<String, ColumnType> config, Dictionary<string, Column> data, String tableName)
    {
        Config = config;
        Data = data;
        Name = tableName;
    }

    public void Print()
    {
        PrintList(Config.Keys.ToList<object>(), MaxWidth);
        if (Data.Count < 0)
            return;

        for (var i = 0; i < Data.First().Value.Data.Count; i++)
        {
            PrintList(Data.GetRow(i), MaxWidth);
        }
    }

    private void PrintList(List<object> list, int width)
    {
        string left = "| ";
        string bottomLine = "";

        foreach (var header in list)
        {
            var strHeader = header.ToString();
            string spaces = new String(' ', width - strHeader.Length + 1);
            bottomLine = left + strHeader + spaces;
            Console.Write(bottomLine);
        }

        Console.WriteLine("|");
        Console.WriteLine(new String('-', bottomLine.Length * list.Count));
    }
}
