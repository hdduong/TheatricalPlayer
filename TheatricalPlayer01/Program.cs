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

            var str = Statement(invoices.First(), dicPlay);
            Console.Write(str);
        }

        private static T ReadFile<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string Statement(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0.0;
            var volumeCredits = 0.0;

            var result = $"Statement for {invoice.Customer}\n";

            foreach (var perf in invoice.Performances)
            {
                volumeCredits += VolumeCreditsFor(perf, plays);

                result += $"    { PlayFor(perf, plays).Name}: {(AmountFor(perf, plays) / 100):C2} ({perf.Audience} seats)\n";
                totalAmount += AmountFor(perf, plays);
            }

            result += $"Amount owed is {(totalAmount / 100):C2}\n";
            result += $"You earned {volumeCredits} credits\n";
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
