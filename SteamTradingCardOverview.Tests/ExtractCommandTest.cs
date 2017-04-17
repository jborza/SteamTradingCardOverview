using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace SteamTradingCardOverview.Tests
{
    [TestClass]
    public class ExtractCommandTest
    {
        [TestMethod]
        public void CollectGamesTest()
        {
            //arrange
            var cmd = new ExtractCommand();
            var lines = File.ReadAllLines(@"testData\badges.txt");
            //act
            var data = cmd.CollectGames(lines);
            //assert
            Assert.AreEqual(4, data.Count());
            Assert.IsFalse(data.Any(p => p.Name == "Zombie Zoeds"));
            Assert.IsTrue(data.Any(p => p.Name == "Big Pharma"));
            Assert.AreEqual(6, data.Single(p => p.Name == "Big Pharma").CardsRemaining);
            Assert.AreEqual(1, data.Single(p => p.Name == "Slime Rancher").CardsRemaining);
        }
    }
}
