using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public abstract class Estimator : Quote
    {
        public Estimator(double value, DateTime date) : base(value, date) { }
    }

    public class MA : Estimator
    {
        int _term;

        public MA(double value, DateTime date, int term) : base(value, date) 
        {
            _term = term;
        }

        public int Term
        {
            get { return _term; }
        }
    }
}
