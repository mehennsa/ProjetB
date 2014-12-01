using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using Engine;
using Tools;

namespace UnitTest
{
    [TestClass]
    public class CoreUT
    {
        [TestMethod]
        public void UTEquityCurve()
        {
            Equity equity = new Equity("Equity1");
            Curve curve = new Curve();
            DateTime start = new DateTime(2000, 1, 1);

            for (int i = 0; i < 100; i++)
                curve.Quotes.Add(start.AddWorkDays(i), new Open(i, start.AddWorkDays(i)));

            equity.Curves.Add(QuoteType.OPEN, curve);

            for (int i = 0; i < 100; i++)
                Assert.IsTrue(equity.Curves[QuoteType.OPEN].Quotes[start.AddWorkDays(i)].Value == i);
        }
    }
}
