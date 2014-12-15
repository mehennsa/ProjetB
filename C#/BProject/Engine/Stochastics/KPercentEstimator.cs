using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Engine.Stochastics
{
    public class KPercentEstimator : BaseStochastic
    {


        #region Stochastic
        
        public override void Compute(Dictionary<QuoteType, Curve> curve)
        {
            // getting the right curve
            Curve periodCurve = (from c in curve
                                 where QuoteType.CLOSE.Equals(c.Key)
                                 select c.Value).SingleOrDefault();
            if (periodCurve == null)
                throw new Exception("Close curve is missing");

            // getting last date
            IQuote lastQuote = (from pc in periodCurve.Quotes
                                     select pc.Value).Max();
            // getting the curveList accordingly to the choosen period
            var curveList = (from pc in periodCurve.Quotes
                             where pc.Key <= lastQuote.Date && pc.Key >= lastQuote.Date.AddWorkDays(-_period)
                             select pc);
            // max
            double maxValue = curveList.Max((pc) => pc.Value).Value;

            double minValue = curveList.Min((pc) => pc.Value.Value);

            // computation for date - 1
            _date = DateTime.Today.AddWorkDays(-1);

            _value = (lastQuote.Value - minValue) / (maxValue - minValue);
        }

        public override object Clone()
        {
            return new KPercentEstimator(this._period, this._value, this._date);
        }

        #endregion

        #region Constructor

        public KPercentEstimator() : base(14) 
        {
        }

        public KPercentEstimator(int period, double value, DateTime date) : base(period, value, date)
        {
        }

        #endregion
    }
}
