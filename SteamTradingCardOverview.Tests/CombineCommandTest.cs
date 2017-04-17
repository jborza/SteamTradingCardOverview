using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SteamTradingCardOverview.Tests
{
    [TestClass]
    public class CombineCommandTest
    {
        [TestMethod]
        public void MergeTest()
        {
            //arrange
            var args = new string[] { "combine", @"testData\out.csv", @"testData\STC_set_data.csv" };
            CombineCommand cc = new CombineCommand();
            var exportInfos = new List<GameInfo>() {
                new GameInfo() { Name = "GameA", CardsRemaining = 3 },
                new GameInfo() { Name = "GameB", CardsRemaining = 1 },
                new GameInfo() { Name = "GameC", CardsRemaining = 4 } };
            var stcInfos = new Dictionary<string, decimal> { { "GameA", 0.11m }, { "GameB", 0.05m }, { "GameC", 0.22m } };
            //act
            var result = cc.Merge(exportInfos, stcInfos);
            //assert
            Assert.Fail("not implemented yet");
        }
    }
}
