namespace SteamTradingCardOverview
{
    public partial class CombineCommand
    {
        public class CardValueInfo
        {
            public string Name;
            public decimal TotalPrice;
            public int CardsRemaining;

            public string CsvPrint()
            {
                return $"\"{Name}\",{CardsRemaining},{TotalPrice}";
            }
        }
    }
}
