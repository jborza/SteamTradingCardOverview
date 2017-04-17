using System;
using System.Collections.Generic;

namespace SteamTradingCardOverview
{
    internal static class ConsoleUtils
    {
        internal static List<string> ReadAllLinesFromConsole()
        {
            var lines = new List<string>();
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                lines.Add(line);
            }

            return lines;
        }
    }
}
