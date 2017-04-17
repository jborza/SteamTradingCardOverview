using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTradingCardOverview
{
    class CombineCommand
    {
        private string exportCsvFilename;
        private string steamToolsCsvFilename;
        public void Execute(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("combine: Arguments missing, expected 2");
                return;
            }
            exportCsvFilename = args[1];
            steamToolsCsvFilename = args[2];
            Merge();
        }

        private void Merge()
        {
            var exportInfos = ReadGameInfosFromExport(exportCsvFilename);
            var stcInfos = ReadStcGameInfos(steamToolsCsvFilename).ToDictionary(k => k.Name, v => v.CardAverage);
            foreach (var exportInfo in exportInfos)
            {
                Console.WriteLine(exportInfo.Name + " / " + exportInfo.CardsRemaining);
                var match = stcInfos[exportInfo.Name];
                Console.WriteLine("matched card average:" + match);
            }
        }

        private IEnumerable<StcGameInfo> ReadStcGameInfos(string filename)
        {
            var parser = new TextFieldParser(new StreamReader(filename));
            parser.SetDelimiters(",");
            parser.ReadLine(); //skip first header line
            string[] fields;
            const int FIELD_NAME = 0;
            const int FIELD_AVERAGE = 7;
            while (!parser.EndOfData)
            {
                fields = parser.ReadFields();
                yield return new StcGameInfo()
                {
                    Name = fields[FIELD_NAME],
                    CardAverage = Decimal.Parse(fields[FIELD_AVERAGE])
                };
            }
        }

        private class StcGameInfo
        {
            public string Name;
            public decimal CardAverage;
        }

        private IEnumerable<GameInfo> ReadGameInfosFromExport(string filename)
        {
            var parser = new TextFieldParser(new StreamReader(filename));
            parser.SetDelimiters(",");
            parser.ReadLine(); //skip first header line
            string[] fields;
            const int FIELD_NAME = 0;
            const int FIELD_REMAINING = 1;
            while (!parser.EndOfData)
            {
                fields = parser.ReadFields();
                yield return new GameInfo()
                {
                    Name = fields[FIELD_NAME],
                    CardsRemaining = int.Parse(fields[FIELD_REMAINING])
                };
            }
        }
    }
}
