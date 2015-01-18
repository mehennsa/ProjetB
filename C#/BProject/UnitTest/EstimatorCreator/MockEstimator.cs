using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.EstimatorCreator
{
    public class MockEstimator : Engine.Estimator
    {
        public MockEstimator()
            : base(0, 0.0, DateTime.Today)
        {

        }

        public MockEstimator(double value, DateTime date)
            : base(0, value, date)
        {

        }

        public override object Clone()
        {
            return new MockEstimator(this.Value, this.Date);
        }

        public override void Compute(Dictionary<Engine.QuoteType, Engine.Curve> curve, Engine.QuoteType quoteType = Engine.QuoteType.NONE)
        {
            
        }
    }
}
