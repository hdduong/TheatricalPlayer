using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TheatricalPlayer01.Models;

namespace TheatricalPlayer01
{
    public class Program
    {
        private static string _dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        static void Main(string[] args)
        {
            var playFilePath = Path.Combine(_dataFolder, "plays.json");
            var invoiceFilePath = Path.Combine(_dataFolder, "invoices.json");

            var plays = ReadFile<List<Play>>(playFilePath);
            var invoices = ReadFile<List<Invoice>>(invoiceFilePath);

            var dicPlay = new Dictionary<string, Play>();
            foreach (var play in plays)
            {
                dicPlay.Add(play.Id, play);
            }

            var str = Statement(new StatementData
            {
                Invoice = invoices.First(),
                Plays = dicPlay
            });
            Console.Write(str);
        }

        private static T ReadFile<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string Statement(StatementData data)
        {
            return RenderPlainText(data);
        }

        public static string StatementHtml(StatementData data)
        {
            return RenderHtml(data);
        }

        public static string RenderPlainText(StatementData data)
        {
            var result = $"Statement for {data.GetCustomer()}\n";
           
            foreach (var perf in data.GetPerformances())
            {
                result += $"    {perf.PlayFor(data.Plays).Name}: {(data.AmountFor(perf) / 100):C2} ({perf.Audience} seats)\n";
            }

            result += $"Amount owed is {(data.TotalAmountFor() / 100):C2}\n";
            result += $"You earned {data.TotalVolumeCreditsFor()} credits\n";
            return result;
        }

        public static string RenderHtml(StatementData data)
        {
            var result = $"<h1>Statement for {data.GetCustomer()}</h1>\n";

            foreach (var perf in data.GetPerformances())
            {
                result += $"    {perf.PlayFor(data.Plays).Name}: {(data.AmountFor(perf) / 100):C2} ({perf.Audience} seats)\n";
            }

            result += $"Amount owed is {(data.TotalAmountFor() / 100):C2}\n";
            result += $"You earned {data.TotalVolumeCreditsFor()} credits\n";
            return result;
        }
    }
}
