using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheatricalPlayer01.Models;

namespace TheatricalPlayer01.Test
{
    [TestClass]
    public class StatementTests
    {
        [TestMethod]
        public void ProduceTest()
        {
            var invoice = new Invoice
            {
                Customer = "BigCo",
                Performances = new List<Performance>
                {
                    new Performance
                    {
                        PlayId = "hamlet",
                        Audience = 55
                    },
                    new Performance
                    {
                        PlayId = "as-like",
                        Audience = 35
                    },
                    new Performance
                    {
                        PlayId = "osthello",
                        Audience = 40
                    }
                }
            };

            var dicPlay = new Dictionary<string, Play>();
            dicPlay.Add("hamlet", new Play
            {
                Id = "hamlet",
                Name = "Hamlet",
                Type = "tragedy"
            });
            dicPlay.Add("as-like", new Play
            {
                Id = "as-like",
                Name = "As You Like It",
                Type = "comedy"
            });
            dicPlay.Add("osthello", new Play
            {
                Id = "osthello",
                Name = "Osthello",
                Type = "tragedy"
            });
            
            var main = Program.Statement(new StatementData
            {
                Invoice = invoice,
                Plays = dicPlay
            });

            var expectedResult = "Statement for BigCo\n";
            expectedResult += "    Hamlet: $650.00 (55 seats)\n";
            expectedResult += "    As You Like It: $580.00 (35 seats)\n";
            expectedResult += "    Osthello: $500.00 (40 seats)\n";
            expectedResult += "Amount owed is $1,730.00\n";
            expectedResult += "You earned 47 credits\n";
            Assert.AreEqual(expectedResult, main);

            var mainHtml = Program.StatementHtml(new StatementData
            {
                Invoice = invoice,
                Plays = dicPlay
            });
            var expectedHtml = "<h1>Statement for BigCo</h1>\n";
            expectedHtml += "    Hamlet: $650.00 (55 seats)\n";
            expectedHtml += "    As You Like It: $580.00 (35 seats)\n";
            expectedHtml += "    Osthello: $500.00 (40 seats)\n";
            expectedHtml += "Amount owed is $1,730.00\n";
            expectedHtml += "You earned 47 credits\n";
            Assert.AreEqual(expectedHtml, mainHtml);
        }
    }
}
