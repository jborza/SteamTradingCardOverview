using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace SteamTradingCardOverview.Tests
{
    [TestClass]
    public class CombineCommandTest
    {
        [TestMethod]
        public void MergeTest()
        {
            //arrange
            CombineCommand cc = new CombineCommand();
            var exportInfos = new List<GameInfo>() {
                new GameInfo() { Name = "GameA", CardsRemaining = 3 },
                new GameInfo() { Name = "GameB", CardsRemaining = 1 },
                new GameInfo() { Name = "GameC", CardsRemaining = 4 } };
            var stcInfos = new List<CombineCommand.StcGameInfo> {
                new CombineCommand.StcGameInfo(){ Name= "GameA", AverageCardValue= 0.11m },
                new CombineCommand.StcGameInfo(){ Name= "GameB", AverageCardValue = 0.05m },
                new CombineCommand.StcGameInfo(){ Name= "GameC", AverageCardValue=0.22m },
                new CombineCommand.StcGameInfo(){ Name= "GameD", AverageCardValue=0.17m }};
            //act
            var result = cc.Merge(exportInfos, stcInfos);
            //assert
            var asArray = result.ToArray();
            Assert.AreEqual(3, asArray.Count());
            Assert.AreEqual("GameA", asArray[0].Name);
            Assert.AreEqual(3, asArray[0].CardsRemaining);
            Assert.AreEqual(0.33m, asArray[0].TotalPrice);
            Assert.AreEqual("GameB", asArray[1].Name);
            Assert.AreEqual(1, asArray[1].CardsRemaining);
            Assert.AreEqual(0.05m, asArray[1].TotalPrice);
            Assert.AreEqual("GameC", asArray[2].Name);
            Assert.AreEqual(4, asArray[2].CardsRemaining);
            Assert.AreEqual(0.88m, asArray[2].TotalPrice);
        }
    }
}
