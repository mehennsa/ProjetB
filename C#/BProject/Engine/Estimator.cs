using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public abstract class Estimator : IQuote
    {
        double _value;
        DateTime _date;

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = Date; }
        }
    }

    public class MA : Estimator
    {
        int _term;

        public int Term
        {
            get { return _term; }
            set { _term = value; }
        }
    }
}
