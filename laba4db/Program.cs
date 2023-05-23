namespace laba4db;

internal static class Program
{
    public static void Main()
    {
        var db = new Database(
            new FileInfo(@"C:\Users\Dell\source\repos\dblaba4\dblaba4\config.json")
            );
        db.Print();
    }
}
