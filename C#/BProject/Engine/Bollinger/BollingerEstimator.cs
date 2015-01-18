using Engine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Engine.Bollinger
{
    // On enregistre ici les écarts types
    // Bande sup = MA * 2SN
    // Bande min = MA * - 2SN
    public class BollingerEstimator : Estimator, IUseOtherEstimator
    {
        #region Fields

        private int _period;

        private Estimator _estimator;

        private const int DefaultPeriod = 20;

        #endregion

        #region .ctor

        public BollingerEstimator(double value, DateTime date, int period)
        {
            _estimator = new MA(value, date, period);
            _value = value;
            _date = date;
            _period = period;
        }

        public BollingerEstimator(double value, DateTime date)
            : this(value, date, DefaultPeriod)
        {

        }

        public BollingerEstimator()
            : this(0, DateTime.Today)
        {

        }

        #endregion

        #region IUseOtherEstimator

        public Estimator UsedEstimator
        {
            get
            {
                return _estimator;
            }
            set
            {
                _estimator = value;
            }
        }

        #endregion

        #region Estimator

        public override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.OPEN)
        {
            if (QuoteType.NONE.Equals(quoteType))
            {
                quoteType = QuoteType.OPEN;
            }

            if (!curve.ContainsKey(quoteType))
            {
                throw new Exception(quoteType.ToString() + " curve is missing!!");
            }

            if (_period <= 1)
            {
                throw new Exception("Period for computations of Bollinger should be greater than 1");
            }

            _estimator.Compute(curve, quoteType);
            double mean = _estimator.Value;
            double sum = 0;
            for (int i = 0; i < _period; i++)
            {
                double val = curve[quoteType].Quotes[_date.AddWorkDays(-i)].Value;
                val -= mean;
                sum += val * val;
            }
            sum /= (_period - 1);
            _value = Math.Sqrt(sum);
        }

        public override object Clone()
        {
            return new BollingerEstimator(this._value, this._date, this._period);
        }

        #endregion
    }
}
