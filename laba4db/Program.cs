namespace laba4db;

internal static class Program
{
    public static void Main()
    {
        var file = new FileInfo(@"C:\Users\Dell\source\repos\laba4db\laba4db\jsconfig1.json");
        var config = DataCreator.GetConfig(file);
        var db = new Database(config, DataCreator.GetTables(config));
        db.Print();
    }
}
//2

