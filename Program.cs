using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace SteamTradingCardOverview
{
    class Program
    {
        static void Main(string[] args)
        {
            var games = CollectGames();
            foreach(var game in games.OrderBy(p=>p.Name))
            {
                Console.WriteLine(game.Name+";"+game.CardsRemaining);
            }
        }

        static IEnumerable<GameInfo> CollectGames() { 
            Status status = Status.LookingForCardDrops;
            string line;
            
            int cardDropsRemaining = 0;
            while ((line = Console.ReadLine()) != null)
            {
                if (status == Status.LookingForCardDrops)
                {
                    Regex dropsRemaining = new Regex("([0-9]+) card drops? remaining");
                    if (dropsRemaining.IsMatch(line))
                    {
                        cardDropsRemaining = int.Parse(dropsRemaining.Match(line).Groups[1].Value);
                        status = Status.LookingForGameName;
                        continue;
                    }
                }
                if(status == Status.LookingForGameName)
                {
                    status = Status.LookingForCardDrops;
                    yield return new GameInfo()
                    {
                        Name = line.Trim(),
                        CardsRemaining = cardDropsRemaining
                    };
                }
            }
        }

        class GameInfo
        {
            public string Name;
            public int CardsRemaining;
        }

        enum Status
        {
            LookingForCardDrops,
            LookingForGameName
        }
    }
}
