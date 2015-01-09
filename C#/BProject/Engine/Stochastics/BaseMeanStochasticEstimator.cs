using Engine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Engine.Stochastics
{
    public abstract class BaseMeanStochasticEstimator : BaseStochastic, IUseOtherEstimator
    {
        #region Fields

        protected Estimator _usedEstimator;

        #endregion

        #region Estimator
        public override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.CLOSE)
        {
            if (QuoteType.NONE.Equals(quoteType))
            {
                quoteType = QuoteType.CLOSE;
            }

            if (!curve.ContainsKey(quoteType))
                throw new Exception("Missing quote curve");


            _value = 0;

            DateTime lastDate = GetLastDateOnCurve(curve[quoteType]);
            Dictionary<QuoteType, Curve> curveCopy = new Dictionary<QuoteType, Curve>(curve);
            double mean = 0;

            for (int i = 0; i < _period; i++)
            {
                curveCopy[quoteType] = curveCopy[quoteType].CreateSubCurve(lastDate.AddWorkDays(-i));
                _usedEstimator.Compute(curveCopy, quoteType);
                mean += _usedEstimator.Value;

            }

            mean /= _period;
            _value = mean;
            _date = lastDate;
        }

 
        public abstract override object Clone();

        #endregion

        #region constructor 

        public BaseMeanStochasticEstimator(int period, double value, DateTime date)
            : base(period, value, date)
        {

        }

        public BaseMeanStochasticEstimator()
            : base()
        {

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
    }
}
