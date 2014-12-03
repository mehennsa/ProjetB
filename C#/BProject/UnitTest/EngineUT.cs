using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using Tools;

namespace UnitTest
{
    [TestClass]
    public class EngineUT
    {
        // Test d'ajout de cours.
        [TestMethod]
        public void UTCurveAddQuotes()
        {
            Curve curve = new Curve();
            DateTime start = new DateTime(2000, 1, 1);

            for (int i = 0; i < 100; i++)
                curve.Quotes.Add(start.AddWorkDays(i), new Open(i, start.AddDays(i)));

            for (int i = 0; i < 100; i++)
                Assert.IsTrue(curve.Quotes.ContainsKey(start.AddWorkDays(i)));

            for (int i = 0; i < 100; i++)
                Assert.IsTrue(curve.Quotes[start.AddWorkDays(i)].Value == i);
        }

        // Test d'ajout de cours de type différent.
        // Une exception doit être levée
        [TestMethod]
        [ExpectedException(typeof(Exception), "Try to add different quote type in a curve")]
        public void UTCurveAddQuotesTypeException()
        {
            Curve curve = new Curve();
            
            curve.Quotes.Add(new Open(1, new DateTime()));
            curve.Quotes.Add(new Close(1, new DateTime()));
        }

        // Test de calcul d'une moyenne mobile
        [TestMethod]
        public void UTMAComputation()
        {
            Curve curve = new Curve();
            DateTime start = new DateTime(2014, 1, 2);

            for (int i = 0; i < 20; i++)
                curve.Quotes.Add(new Open(i, start.AddWorkDays(i)));

            MA ma20 = new MA(start.AddWorkDays(19), 20);

            ma20.Compute(curve);

            Assert.IsTrue(ma20.Value == 9.5);
        }
    }
}
