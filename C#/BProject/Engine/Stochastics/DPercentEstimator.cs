using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Interface;
using Tools;

namespace Engine.Stochastics
{
    public class DPercentEstimator : BaseStochastic, IUseOtherEstimator
    {

        #region Fields

        private Estimator _usedEstimator;

        #endregion

        #region Stochastic

        public override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.CLOSE)
        {
            if (!curve.ContainsKey(QuoteType.CLOSE))
                throw new Exception("Missing close curve");

            _value = 0;

            DateTime lastDate = (from c in curve[QuoteType.CLOSE].Quotes
                                     select c.Key).Max();
            Dictionary<QuoteType, Curve> curveCopy = new Dictionary<QuoteType, Curve>(curve);
            double mean = 0;

            for (int i = 0; i < _period; i++)
            {
                curveCopy[QuoteType.CLOSE] = curveCopy[QuoteType.CLOSE].CreateSubCurve(lastDate.AddWorkDays(-i));
                //_usedEstimator.Compute(curveCopy);
                mean += _usedEstimator.Value;

            }

            mean /= _period;
            _value = mean;
            _date = lastDate;
        }

        public override object Clone()
        {
            return new DPercentEstimator(this._period, this._value, this._date);
        }

        #endregion

        #region IUseOtherEstimator
        
        public Estimator UsedEstimator
        {
            get
            {
                return _usedEstimator;
            }
            set
            {
                _usedEstimator = value;
            }
        }

        #endregion

        #region Constructor

        public DPercentEstimator() : base(3)
        {
            _usedEstimator = new KPercentEstimator();
        }

        public DPercentEstimator(int period, double value, DateTime date) : base(period, value, date)
        {
            _usedEstimator = new KPercentEstimator();
        }

        #endregion
    }
}
