namespace laba4db;

public class Column
{
    public String Name { get; set; }
    public ColumnType Type { get; set; }
    public List<object> Data { get; set; }
    public Tuple<int, int> Shape => Tuple.Create(Data.Count, 0);
    public int MaxWidth
    {
        get
        {
            int max = 0;

            foreach (var element in Data)
            {
                var len = element.ToString().Length;
                max = Math.Max(len, max);
            }

            return max;
        }
    }

    public Column(String name, ColumnType type, List<object> data)
    {
        Name = name;
        Type = type;
        Data = data;
    }
}
