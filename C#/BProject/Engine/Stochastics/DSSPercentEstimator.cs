using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Stochastics
{
    public class DSSPercentEstimator : BaseMeanStochasticEstimator
    {
        #region Stochastic
        
        public override object Clone()
        {
            return new DSSPercentEstimator(this._period, this._value, this._date);
        }

        #endregion

        #region Constructor

        public DSSPercentEstimator()
            : base()
        {
            _period = 3;
            _usedEstimator = new DSPercentEstimator();
        }

        public DSSPercentEstimator(int period, double value, DateTime date)
            : base(period, value, date)
        {
            _usedEstimator = new DSPercentEstimator();
        }

        #endregion
    }
}
