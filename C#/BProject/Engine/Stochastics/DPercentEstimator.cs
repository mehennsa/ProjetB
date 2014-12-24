using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Interface;
using Tools;

namespace Engine.Stochastics
{
    public class DPercentEstimator : BaseMeanStochasticEstimator
    {

        #region Stochastic

        public override object Clone()
        {
            return new DPercentEstimator(this._period, this._value, this._date);
        }

        #endregion

        #region Constructor

        public DPercentEstimator() : base()
        {
            _period = 3;
            _usedEstimator = new KPercentEstimator();
        }

        public DPercentEstimator(int period, double value, DateTime date) : base(period, value, date)
        {
            _usedEstimator = new KPercentEstimator();
        }

        #endregion
    }
}
