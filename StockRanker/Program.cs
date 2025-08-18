// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using StockRanker;

internal class Program
{
    private static void Main()
    {
        List<StockInfo> infos = [];
        string data = "";
        string filename = "stocks.json";
        if (!File.Exists(filename))
        {
            Console.WriteLine("File was not found.");
        }
        else
        {
            data = File.ReadAllText("stocks.json");
            infos = JsonSerializer.Deserialize<List<StockInfo>>(data)!;
        }

        while(true)
        {
            ModifyStock(infos);
        }
    }

    public static void ModifyStock(List<StockInfo> infos)
    {
        Console.Clear();
        Console.WriteLine("Stocks and ranks:");
        foreach (var info in infos)
        {
            Console.WriteLine(info);
        }

        Console.WriteLine("Write a ticker to modify!");
        string tickerName = Console.ReadLine()!;
        Console.WriteLine($"Selected stock is: {tickerName}");

        StockInfo selectedInfo = infos.Where(info => info.Ticker == tickerName).FirstOrDefault()!;
        Console.WriteLine("Add a rank to the selected stock!");
        int givenRank = Convert.ToInt32(Console.ReadLine());
        foreach (var info in infos.Where(info => info.Rank == givenRank))
        {
            info.Rank = selectedInfo.Rank;
        }
        selectedInfo.Rank = givenRank;

        Console.WriteLine("Modified stock rank");
        foreach (var info in infos)
        {
            Console.WriteLine(info);
        }

        string fileData = JsonSerializer.Serialize(infos);
        File.WriteAllText("stocks.json", fileData);
        Console.WriteLine("Changes were saved.");
    }
}