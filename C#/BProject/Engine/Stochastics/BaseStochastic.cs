﻿using System;
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

        public abstract override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.NONE);

        public abstract override object Clone();

        #endregion

        #region Constructor

        public BaseStochastic(int period)
            : base(period, DateTime.Today.AddWorkDays(-1))
        {
        }

        public BaseStochastic()
        {
            _date = DateTime.Today.AddWorkDays(-1);
        }

        public BaseStochastic(int period, double value, DateTime date)
            : base(period, value, date)
        {
            _period = period;
        }

        #endregion

        #region Utils Methods

        protected DateTime GetLastDateOnCurve(Curve curve)
        {
            return (from c in curve.Quotes
                    select c.Key).Max();
        }

        #endregion
    }
}
