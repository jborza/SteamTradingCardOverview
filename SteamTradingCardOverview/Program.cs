using System;

namespace SteamTradingCardOverview
{
    class Program
    {
        static void Main(string[] args)
        {
           if(args.Length == 0)
            {
                Console.WriteLine("Usage: steamtradingcardoverview [extract] [combine GENERATED.CSV STC_DATA.csv]");
                Console.WriteLine("commands:");
                Console.WriteLine("  extract - reads dump from the standard input and generates the CSV as \"game\";amount_of_cards");
                Console.WriteLine("  combine - combines an output csv with STC_set_data.csv from http://steam.tools");
                return;
            }
            if (args[0] == "extract")
            {
                new ExtractCommand().Execute();
            }
            else if (args[0] == "combine")
            {
                new CombineCommand().Execute(args);
            }
        }        
    }
}
