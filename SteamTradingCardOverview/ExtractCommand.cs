using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SteamTradingCardOverview
{
    class ExtractCommand
    {
        public void Execute()
        {
            var lines = ConsoleUtils.ReadAllLinesFromConsole();
            var games = CollectGames(lines);
            Console.WriteLine("\"game\",\"card drops remaining\"");
            foreach (var game in games.OrderBy(p => p.Name))
            {
                Console.WriteLine($"\"{game.Name}\",{game.CardsRemaining}");
            }
        }

        internal IEnumerable<GameInfo> CollectGames(IEnumerable<string> lines)
        {
            ExtractParserStatus status = ExtractParserStatus.LookingForCardDrops;

            int cardDropsRemaining = 0;
            foreach(var line in lines)
            {
                if (status == ExtractParserStatus.LookingForCardDrops)
                {
                    Regex dropsRemaining = new Regex("([0-9]+) card drops? remaining");
                    if (dropsRemaining.IsMatch(line))
                    {
                        cardDropsRemaining = int.Parse(dropsRemaining.Match(line).Groups[1].Value);
                        status = ExtractParserStatus.LookingForGameName;
                        continue;
                    }
                }
                if (status == ExtractParserStatus.LookingForGameName)
                {
                    status = ExtractParserStatus.LookingForCardDrops;
                    yield return new GameInfo()
                    {
                        Name = line.Trim(),
                        CardsRemaining = cardDropsRemaining
                    };
                }
            }
        }

        private IEnumerable<GameInfo> CollectGames()
        {
            ExtractParserStatus status = ExtractParserStatus.LookingForCardDrops;
            string line;

            int cardDropsRemaining = 0;
            while ((line = Console.ReadLine()) != null)
            {
                if (status == ExtractParserStatus.LookingForCardDrops)
                {
                    Regex dropsRemaining = new Regex("([0-9]+) card drops? remaining");
                    if (dropsRemaining.IsMatch(line))
                    {
                        cardDropsRemaining = int.Parse(dropsRemaining.Match(line).Groups[1].Value);
                        status = ExtractParserStatus.LookingForGameName;
                        continue;
                    }
                }
                if (status == ExtractParserStatus.LookingForGameName)
                {
                    status = ExtractParserStatus.LookingForCardDrops;
                    yield return new GameInfo()
                    {
                        Name = line.Trim(),
                        CardsRemaining = cardDropsRemaining
                    };
                }
            }
        }

        private enum ExtractParserStatus
        {
            LookingForCardDrops,
            LookingForGameName
        }
    }
}
