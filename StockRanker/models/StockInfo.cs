using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StockRanker
{
	public class StockInfo
	{
		[JsonPropertyName("Ticker")]
		public string Ticker { get; set; }

		[JsonPropertyName("Rank")]
		public int Rank { get; set; }

		public override string ToString()
		{
			return $"{Ticker},{Rank}";
		}

		public StockInfo()
		{
			Ticker = "";

			Rank = 0;
		}

	}
}
