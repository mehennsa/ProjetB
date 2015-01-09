using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using Tools;
using System.Linq;

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
            Dictionary<QuoteType, Curve> curves = new Dictionary<QuoteType,Curve>();
            Curve curve = new Curve();
            DateTime start = new DateTime(2014, 1, 2);

            for (int i = 0; i < 20; i++)
                curve.Quotes.Add(new Close(i, start.AddWorkDays(i)));
                //curve.Quotes.Add(new Open(100 * Math.Sin(i), start.AddWorkDays(i)));

            curves.Add(QuoteType.CLOSE, curve);

            MA ma20 = new MA(start.AddWorkDays(19), 20);

            ma20.Compute(curves);

            Assert.IsTrue(Math.Round(ma20.Value, 4) == 0.4264);
        }

        // Test de calcul d'une moyenne mobile exponentielle
        [TestMethod]
        public void UTEMAComputation()
        {
            Dictionary<QuoteType, Curve> curves = new Dictionary<QuoteType, Curve>();
            Curve curve = new Curve();
            DateTime start = new DateTime(2014, 1, 2);

            for (int i = 0; i < 20; i++)
                //curve.Quotes.Add(new Close(i, start.AddWorkDays(i)));
                curve.Quotes.Add(new Open(100 * Math.Sin(i), start.AddWorkDays(i)));

            curves.Add(QuoteType.CLOSE, curve);

            EMA ema20 = new EMA(start.AddWorkDays(19), 20);

            ema20.Compute(curves);

            Assert.IsTrue(Math.Round(ema20.Value, 4) == -6.3699);
        }

        // Test de calcul d'une moyenne mobile pondérée
        [TestMethod]
        public void UTWMAComputation()
        {
            Dictionary<QuoteType, Curve> curves = new Dictionary<QuoteType, Curve>();
            Curve curve = new Curve();
            DateTime start = new DateTime(2014, 1, 2);

            for (int i = 0; i < 20; i++)
                curve.Quotes.Add(new Close(100*Math.Sin(i), start.AddWorkDays(i)));

            curves.Add(QuoteType.CLOSE, curve);

            WMA wma20 = new WMA(start.AddWorkDays(19), 20);

            wma20.Compute(curves);

            Assert.IsTrue(Math.Round(wma20.Value, 4) == -7.3910);
        }


        

        // Test de calcul d'une moyenne mobile de Hull
        [TestMethod]
        public void UTHMAComputation()
        {
            Dictionary<QuoteType, Curve> curves = new Dictionary<QuoteType, Curve>();
            Curve curve = new Curve();
            DateTime start = new DateTime(2014, 1, 2);
            for (int i = 0; i < 20; i++)
                curve.Quotes.Add(new Open(100 * Math.Sin(i), start.AddWorkDays(i)));

            curves.Add(QuoteType.OPEN, curve);

            HMA hma20 = new HMA(start.AddWorkDays(19), 20);

            hma20.Compute(curves);

            Assert.IsTrue(Math.Round(hma20.Value, 4) == -21.6408);
        }

        

    // Test de calcul du RSI avec moyenne mobile sur une période de 14jours
        [TestMethod]
        public void UTRSIComputation()
        {
            Dictionary<QuoteType, Curve> curves = new Dictionary<QuoteType, Curve>();
            Curve curve = new Curve();
            DateTime start = new DateTime(2014, 1, 2);
            for (int i = 0; i < 14; i++)
                curve.Quotes.Add(new Close(10*(1+(0.1)*Math.Pow((-1.1),i)), start.AddWorkDays(i)));

            curves.Add(QuoteType.CLOSE, curve);

            RSI rsi14 = new RSI(start.AddWorkDays(14), 14);

            rsi14.Compute(curves);

            Assert.IsTrue(Math.Round(rsi14.Value, 4) == 49.5202);
        }

        // Test de calcul du ROC sur une période de 14jours
        [TestMethod]
        public void UTROCComputation()
        {
            Dictionary<QuoteType, Curve> curves = new Dictionary<QuoteType, Curve>();
            Curve curve = new Curve();
            DateTime start = new DateTime(2014, 1, 2);

            for (int i = 0; i < 10; i++)
                curve.Quotes.Add(new Close(10 * (1 + (0.1) * Math.Pow((-1.1), i)), start.AddWorkDays(i)));

            curves.Add(QuoteType.CLOSE, curve);

            ROC roc10 = new ROC(start.AddWorkDays(10), 10);
            roc10.Compute(curves);
            Assert.IsTrue(Math.Round(roc10.Value, 4) == -30.5268);
        }

        // Test de calcul du MACD sur une période de 14jours
        [TestMethod]
        public void UTMACDComputation()
        {
            Dictionary<QuoteType, Curve> curves = new Dictionary<QuoteType, Curve>();
            Curve curve = new Curve();
            DateTime start = new DateTime(2014, 1, 2);

            for (int i = 0; i < 10; i++)
                curve.Quotes.Add(new Close(10 * (1 + (0.1) * Math.Pow((-1.1), i)), start.AddWorkDays(i)));

            curves.Add(QuoteType.CLOSE, curve);

            ROC roc10 = new ROC(start.AddWorkDays(10), 10);
            roc10.Compute(curves);
            Assert.IsTrue(Math.Round(roc10.Value, 4) == -30.5268);
        }

        // Test pour la création d'une sous courbe
        [TestMethod]
        public void CreateSubCurveTest()
        {
            // Création d'une courbe
            Curve firstCurve = new Curve();
            DateTime firstDate = new DateTime(2014, 12, 01);
            DateTime today = new DateTime(2014, 12, 15);

            // Création de nombres aléatoire

            for (DateTime d = firstDate; d <= today; d = d.AddWorkDays(1))
            {
                Random r = new Random();

                firstCurve.Quotes.Add(new Open(r.NextDouble()*100, d));
            }

            DateTime lastDate = new DateTime(2014, 12, 12);

            Curve subCurve = firstCurve.CreateSubCurve(lastDate);

            Assert.AreEqual(lastDate.WorkingDaysFromBetweenDates(firstDate) + 1, firstCurve.Quotes.Count);

            Assert.AreEqual(firstCurve.Quotes[lastDate].Value, subCurve.Quotes[lastDate].Value);

            DateTime lastDateInCurve = (from q in subCurve.Quotes
                                        select q).Max((q) => q.Key);
            Assert.AreEqual(lastDate, lastDateInCurve);

        }

        [TestMethod]
        public void CompareToGreaterTest()
        {
            Open firstOpen = new Open(100, DateTime.Today);

            Open otherOpen = new Open(90, DateTime.Today);

            Assert.AreEqual(1, firstOpen.CompareTo(otherOpen));

        }

        [TestMethod]
        public void CompareToLowerTest()
        {
            Open firstOpen = new Open(90, DateTime.Today);

            Open otherOpen = new Open(100, DateTime.Today);

            Assert.AreEqual(-1, firstOpen.CompareTo(otherOpen));

        }

        [TestMethod]
        public void CompareToEqualTest()
        {
            Open firstOpen = new Open(100, DateTime.Today);

            Open otherOpen = new Open(100, DateTime.Today);

            Assert.AreEqual(0, firstOpen.CompareTo(otherOpen));

        }

        #region QuoteImplementation Test

        [TestMethod]
        public void QuoteImplentationTest()
        {
            Open open = QuoteCreatorHelper.CreateQuote(QuoteType.OPEN) as Open;

            Assert.AreEqual(typeof(Open), open.GetType());
            Assert.AreEqual(0, open.Value);
            Assert.AreEqual(DateTime.Today, open.Date);

            Close close = QuoteCreatorHelper.CreateQuote(QuoteType.CLOSE) as Close;

            Assert.AreEqual(typeof(Close), close.GetType());

            High high = QuoteCreatorHelper.CreateQuote(QuoteType.HIGH) as High;

            Assert.AreEqual(typeof(High), high.GetType());

            Low low = QuoteCreatorHelper.CreateQuote(QuoteType.LOW) as Low;

            Assert.AreEqual(typeof(Low), low.GetType());

            Volume vol = QuoteCreatorHelper.CreateQuote(QuoteType.VOLUME) as Volume;

            Assert.AreEqual(typeof(Volume), vol.GetType());

            Open newOpen = QuoteCreatorHelper.CreateQuote(QuoteType.OPEN, 100.2, new DateTime(2014, 12, 19)) as Open;

            Assert.AreEqual(typeof(Open), newOpen.GetType());
            Assert.AreEqual(100.2, newOpen.Value);
            Assert.AreEqual(new DateTime(2014, 12, 19), newOpen.Date);

        }

        #endregion

        [TestMethod]
        public void ConcatenateCurveTest()
        {
            Curve curve = new Curve();

            DateTime startDate = new DateTime(2014, 12, 01);

            DateTime firstEndDate = new DateTime(2014, 12, 05);
            Random r = new Random();

            for (DateTime d = startDate; d <= firstEndDate; d = d.AddWorkDays(1))
            {
                curve.Quotes.Add(new Close(r.Next(), d));
            }

            Curve otherCurve = new Curve();
            DateTime secondEndDate = new DateTime(2014, 12, 11);
            for (DateTime d = firstEndDate; d <= secondEndDate; d = d.AddWorkDays(1))
            {
                otherCurve.Quotes.Add(new Close(r.Next(), d));
            }

            Assert.AreEqual(5, curve.Quotes.Keys.Count);

            curve.ConcatenateCurve(otherCurve);

            Assert.AreEqual(9, curve.Quotes.Keys.Count);
            Assert.AreEqual(curve.Quotes[secondEndDate], otherCurve.Quotes[secondEndDate]);
        }
    }
}
