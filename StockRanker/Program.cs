// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using StockRanker;
using System.Text.Json.Serialization;

List<StockInfo> infos = new List<StockInfo>();
string data = "";
string filename = "stocks.txt";
if(!File.Exists(filename))
{
    Console.WriteLine("File was not found.");
}
else
{
    data = File.ReadAllText("stocks.txt");
    infos = JsonSerializer.Deserialize<List<StockInfo>>(data);
    Console.WriteLine("Stock list was filled.");
}

foreach(var info in infos)
{
    Console.WriteLine(info);
}

Console.WriteLine("Write a ticker to modify!");
string tickerName = Console.ReadLine();
Console.WriteLine($"Selected stock is: {tickerName}");

StockInfo selectedInfo = infos.Where(info => info.Ticker == tickerName).FirstOrDefault();
Console.WriteLine("Add a rank to the selected stock!");
int givenRank = Convert.ToInt32(Console.ReadLine());
foreach(var info in infos.Where(info => info.Rank == givenRank ))
{
    info.Rank = selectedInfo.Rank;
}
selectedInfo.Rank = givenRank;

Console.WriteLine("Modified stock rank");
foreach(var info in infos)
{
    Console.WriteLine(info);
}

