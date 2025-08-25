// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using StockRanker;

namespace StockRanker
{

    internal class Program
    {
        private static void Main()
        {
            bool run = true;
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

            while(run)
            {
                Console.WriteLine("What do you want to do? \n 0 - list stocks. \n 1 - add stock \n 2 - modify stock \n 3 - delete stock \n 4 - quit");
                int menu = Convert.ToInt32(Console.ReadLine());
                switch(menu)
                {
                    case 0:
                        ListStock(infos);
                        break;
                    case 1:
                        AddStock(ref infos);
                        break;
                    case 2:
                        ModifyStock(ref infos);
                        break;
                    case 3:
                        DeleteStock(ref infos);
                        break;
                    case 4:
                        run = false;
                        break;
                    default:
                        break;

                }
            
            }
        }
        public static void ModifyStock(ref List<StockInfo> infos)
        {
            Console.Clear();
            Console.WriteLine("Stocks and ranks:");
            ListStock(infos);

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
            ListStock(infos);

            string fileData = JsonSerializer.Serialize(infos);
            File.WriteAllText("stocks.json", fileData);
            Console.WriteLine("Changes were saved.");
        }

        public static void AddStock(ref List<StockInfo> infos)
        {
            Console.Clear();
            System.Console.WriteLine("Name of the stock:");
            string name = Console.ReadLine()!;
            System.Console.WriteLine("Would you like to add a rank to the stock? \n y/n");
            string addRank = Console.ReadLine()!;
            int rank = 0;
            if(addRank == "n")
            {
                rank = infos.Count;
            }
            else
            {
                System.Console.WriteLine("Type the rank here:");
                rank = Convert.ToInt32(Console.ReadLine());
            }
            StockInfo info = new StockInfo(name, rank);
            infos.Add(info);

            string fileData = JsonSerializer.Serialize(infos);
            File.WriteAllText("stocks.json", fileData);
            Console.WriteLine("Changes were saved.");
        }

        public static void ListStock(List<StockInfo> infos)
        {
            Console.Clear();
            IEnumerable<StockInfo> showInfos = from info in infos orderby info.Rank ascending select info;
            foreach (var info in showInfos)
            {
                Console.WriteLine(info);
            }
        }

        public static void DeleteStock(ref List<StockInfo> infos)
        {
            Console.Clear();
            Console.WriteLine("Stocks and ranks:");
            ListStock(infos);
            Console.WriteLine("Write a ticker to delete!");
            string tickerName = Console.ReadLine()!;
            infos.RemoveAll(info => info.Ticker == tickerName);
        }
    }

}
