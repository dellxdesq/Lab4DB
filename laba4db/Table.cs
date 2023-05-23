using System.Data.Common;
using System.Text.Json;

namespace laba4db;

public class Table
{
    private String Name { get; set; }
    public Dictionary<String, Column> Data { get; set; }
    public List<List<object>> RawData { get; set; }
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

    public Table(Dictionary<String, ColumnType> config, FileInfo table, String tableName)
    {
        Data = new Dictionary<string, Column>();
        Config = config;
        RawData = GetTable(table);

        var i = 0;
        foreach (var (name, type) in Config)
        {
            Data.TryAdd(name, new Column(name, type, GetColumn(RawData, i)));
            i++;
        }
    }

    private List<List<object>> GetTable(FileInfo table)
    {
        var fileData = new List<List<object>>();

        var fileRows = File.ReadAllLines(table.FullName);
        foreach (var str in fileRows)
        {
            var list = new List<object>();
            var splitString = str.Split(",");

            foreach (var item in splitString)
            {
                list.Add(item);
            }

            fileData.Add(list);
        }

        return fileData;
    }

    public List<object> GetColumn(List<List<object>> data, int index)
    {
        var column = new List<object>();

        for (var i = 0; i < data.Count; ++i)
        {
            column.Add(data[i][index]);
        }

        return column;
    }

    public void Print()
    {
        PrintList(Config.Keys.ToList<object>(), MaxWidth);
        foreach (var list in RawData)
        {
            PrintList(list, MaxWidth);
        }
    }

    private void PrintList(List<object> list, int width)
    {
        string left = "| ";
        string bottomLine = "";

        foreach (var header in list)
        {
            var strHeader = header.ToString();
            string spaces = new String(' ', width - strHeader.Length);
            bottomLine = left + strHeader + spaces;
            Console.Write(bottomLine);
        }

        Console.WriteLine("|");
        Console.WriteLine(new String('-', bottomLine.Length * list.Count));
    }
}
