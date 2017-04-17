using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SteamTradingCardOverview
{
    public partial class CombineCommand
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
            var exportInfos = ReadGameInfosFromExport(exportCsvFilename);
            var stcInfos = ReadStcGameInfos(steamToolsCsvFilename);
            var data = Merge(exportInfos,stcInfos);
            foreach(var line in PrettyPrint(data))
                Console.WriteLine(line);
        }

        internal IEnumerable<string> PrettyPrint(IEnumerable<CardValueInfo> cards)
        {
            yield return "\"name\",\"cards remaining\",\"total price\"";
            foreach (var card in cards)
                yield return card.CsvPrint();
        }

        internal IEnumerable<CardValueInfo> Merge(IEnumerable<GameInfo> exportInfos, IEnumerable<StcGameInfo> stcInfos)
        {
            return from exp in exportInfos
                   join stc in stcInfos on exp.Name equals stc.Name
                   select new CardValueInfo() { Name = exp.Name, CardsRemaining = exp.CardsRemaining, TotalPrice = exp.CardsRemaining * stc.AverageCardValue };
        }

        private IEnumerable<T> ReadItemsFromCsv<T>(string filename, Func<string[], T> createItemFromFields)
        {
            var parser = new TextFieldParser(new StreamReader(filename));
            parser.SetDelimiters(",");
            parser.ReadLine(); //skip first header line
            string[] fields;

            while (!parser.EndOfData)
            {
                fields = parser.ReadFields();
                yield return createItemFromFields(fields);
            }
        }

        private IEnumerable<StcGameInfo> ReadStcGameInfos(string filename)
        {
            const int FIELD_NAME = 0;
            const int FIELD_AVERAGE = 7;
            return ReadItemsFromCsv(filename, fields => new StcGameInfo()
            {
                Name = fields[FIELD_NAME],
                AverageCardValue = Decimal.Parse(fields[FIELD_AVERAGE])
            });
        }

        private IEnumerable<GameInfo> ReadGameInfosFromExport(string filename)
        {
            const int FIELD_NAME = 0;
            const int FIELD_REMAINING = 1;
            return ReadItemsFromCsv(filename, fields => new GameInfo()
            {
                Name = fields[FIELD_NAME],
                CardsRemaining = int.Parse(fields[FIELD_REMAINING])
            });
        }
    }
}
