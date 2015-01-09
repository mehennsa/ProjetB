using Engine;
using Engine.Stochastics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace UnitTest.Stochastics
{
    [TestClass]
    public class KPercentUT
    {

        #region Fields

        private static KPercentEstimator _estimator;

        private static Dictionary<QuoteType, Curve> _curve;

        #endregion

        [ClassInitialize]
        public static void SetUpTest(TestContext context)
        {
            _estimator = new KPercentEstimator();

            _curve = new Dictionary<QuoteType, Curve>();

            Curve closeCurve = new Curve();

            DateTime startDate = new DateTime(2014, 12, 01);

            DateTime endDate = new DateTime(2014, 12, 30);

            Random r = new Random();

            for (DateTime d = startDate; d <= endDate; d = d.AddWorkDays(1))
            {
                closeCurve.Quotes.Add(new Close(r.NextDouble() * 100, d));
            }
            _curve[QuoteType.CLOSE] = closeCurve;
        }

        [TestMethod]
        public void KPercentComputeTest()
        {
            _estimator.Compute(_curve);
        }
    }
}
