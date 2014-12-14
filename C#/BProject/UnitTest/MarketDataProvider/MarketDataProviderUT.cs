using System;
using Services;
using Services.MarketDataProvider;
using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.MarketDataProviderUT
{
    [TestClass]
    public class MarketDataProviderUT
    {
        [TestMethod]
        public void UTgetLastMarketData()
        {
            //Test la mise à jour du booléen uptoDate dans le cas où l'on a as pas demandé la dernière quote
            MarketDataProvider bla = new MarketDataProvider();
            Curve NotUpToDateCurve = bla.getLastMarketData("MC.PA", QuoteType.OPEN, new DateTime(2011, 05, 22), new DateTime(2012, 06, 13));
            Assert.IsFalse(NotUpToDateCurve.IsUpToDate);
            //Vérifie la valeur du cours open sur la dernière date
            Assert.IsTrue(NotUpToDateCurve.Quotes[new DateTime(2012, 06, 13)].Value == 119.65);
            Assert.IsTrue(NotUpToDateCurve.Quotes[new DateTime(2011, 10, 13)].Value == 113.6);

            //Test la mise à jour du booléen uptoDate dans le cas où l'on a as la dernière quote
            Curve UpToDateCurve = bla.getLastMarketData("MC.PA", QuoteType.OPEN, new DateTime(2011, 05, 22), DateTime.Now.AddDays(1));
            Assert.IsTrue(UpToDateCurve.IsUpToDate);
        }
    }
}
