using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TheatricalPlayer01.Models;

namespace TheatricalPlayer01
{
    class Program
    {
        private static string _dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        static void Main(string[] args)
        {
            var playFilePath = Path.Combine(_dataFolder, "plays.json");
            var invoiceFilePath = Path.Combine(_dataFolder, "invoices.json");

            var plays = ReadFile<List<Play>>(playFilePath);
            var invoices = ReadFile<List<Invoice>>(invoiceFilePath);
        }

        private static T ReadFile<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
