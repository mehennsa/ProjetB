using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;

namespace UnitTest
{
    [TestClass]
    public class EngineUnitTest
    {
        // Test d'ajout de cours.
        [TestMethod]
        public void UTCurveAddQuotes()
        {
            Curve curve = new Curve();

            for (int i = 0; i < 100; i++)
                curve.Quotes.Add(new Open(i, new DateTime()));

            for (int i = 0; i < 100; i++)
                Assert.IsTrue(curve.Quotes.Contains(new Open(i, new DateTime())));
        }

        // Test d'ajout de cours de type différent.
        // Une exception doit être levée
        [TestMethod]
        [ExpectedException(typeof(Exception), "Try to add different quote type in a curve")]
        public void UTCurveAddQuotesException()
        {
            Curve curve = new Curve();
            curve.Quotes.Add(new Open(1, new DateTime()));

            curve.Quotes.Add(new Close(1, new DateTime()));
        }
    }
}
