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

        public static string RenderPlainText(StatementData data)
        {
            var result = $"Statement for {data.Invoice.Customer}\n";
           
            foreach (var perf in data.Invoice.Performances)
            {
                result += $"    { PlayFor(perf, data.Plays).Name}: {(AmountFor(perf, data.Plays) / 100):C2} ({perf.Audience} seats)\n";
            }

            result += $"Amount owed is {(TotalAmountFor(data) / 100):C2}\n";
            result += $"You earned {TotalVolumeCreditsFor(data)} credits\n";
            return result;
        }

        public static double TotalAmountFor(StatementData data)
        {
            var result = 0.0;
            foreach (var perf in data.Invoice.Performances)
            {
                result += AmountFor(perf, data.Plays);
            }
            return result;
        }

        public static double TotalVolumeCreditsFor(StatementData data)
        {
            var result = 0.0;
            foreach (var perf in data.Invoice.Performances)
            {
                result += VolumeCreditsFor(perf, data.Plays);
            }
            return result;
        }

        public static double VolumeCreditsFor(Performance aPerformance, Dictionary<string, Play> plays)
        {
            double result = Math.Max(aPerformance.Audience - 30, 0);
            if (PlayFor(aPerformance, plays).Type.Equals("comedy", StringComparison.InvariantCultureIgnoreCase))
            {
                result += Math.Floor(aPerformance.Audience / 5.0);
            }
            return result;
        }

        public static Play PlayFor(Performance aPerformance, Dictionary<string, Play> plays)
        {
            return plays[aPerformance.PlayId];
        }

        public static int AmountFor(Performance aPerformance, Dictionary<string, Play> plays)
        {
            var result = 0;
            switch (PlayFor(aPerformance, plays).Type)
            {
                case "tragedy":
                    result = 40000;
                    if (aPerformance.Audience > 30)
                    {
                        result += 1000 * (aPerformance.Audience - 30);
                    }
                    break;
                case "comedy":
                    result = 30000;
                    if (aPerformance.Audience > 20)
                    {
                        result += 10000 + 500 * (aPerformance.Audience - 20);
                    }
                    result += 300 * aPerformance.Audience;
                    break;
                default:
                    throw new Exception($"Unknown {PlayFor(aPerformance, plays).Type} ");
            }

            return result;
        }

    }
}
