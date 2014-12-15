using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Interface;
using Tools;

namespace Engine.Stochastics
{
    /// <summary>
    /// Paramètrage de base des stochastiques
    /// </summary>
    public abstract class BaseStochastic : Estimator
    {

        #region Fields & Properties
        /// <summary>
        /// Period for computation
        /// </summary>
        protected int _period;

        public int Period
        {
            get { return _period; }

            set { _period = value; }
        }

        #endregion


        #region Estimator

        public abstract override void Compute(Dictionary<QuoteType, Curve> curve);

        public abstract override object Clone();

        #endregion

        #region Constructor

        public BaseStochastic(int period)
            : base(DateTime.Today.AddWorkDays(-1))
        {
            _period = period;
        }

        public BaseStochastic() : base(DateTime.Today.AddWorkDays(-1))
        {
        }

        public BaseStochastic(int period, double value, DateTime date)
            : base(value, date)
        {
            _period = period;
        }

        #endregion
    }
}
