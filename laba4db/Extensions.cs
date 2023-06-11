using System.ComponentModel;
using System.Globalization;

namespace laba4db;

public static class Extensions
{
    public static List<string> GetColumn(this List<List<string>> data, int index)
    {
        var column = new List<string>();

        for (var i = 0; i < data.Count; ++i)
        {
            column.Add(data[i][index]);
        }

        return column;
    }

    public static object? Cast(this string str, ColumnType type)
    {
        return Convert.ChangeType(str, Type.GetType("System." + type.ToString()), CultureInfo.InvariantCulture);
    }

    public static List<object> GetRow(this Dictionary<String, Column> data, int index)
    {
        var row = new List<object>();

        foreach (var (name, column) in data)
        {
            row.Add(column.Data[index]);
        }

        return row;
    }
}
