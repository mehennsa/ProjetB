using Engine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Engine.Stochastics
{
    public class DSPercentEstimator : BaseMeanStochasticEstimator
    {

        #region Ctor

        public DSPercentEstimator()
        {
            _usedEstimator = new DPercentEstimator();
            _period = 3;
        }

        public DSPercentEstimator(double value, DateTime date)
            : base(3, value, date)
        {
            _usedEstimator = new DSPercentEstimator();
        }

        public DSPercentEstimator(int period, double value, DateTime date)
            : base(period, value, date)
        {
            _usedEstimator = new DSPercentEstimator();
        }

        #endregion

        #region Base Stochastic

        public override object Clone()
        {
            return new DSPercentEstimator(this._period, this._value, this._date);
        }

        #endregion

    }
}
