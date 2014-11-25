using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public interface IQuote
    {
        double Value { get; }
        DateTime Date { get; }
    }

    public abstract class Quote: IQuote
    {
        double _value;
        DateTime _date;

        protected Quote(double value, DateTime date)
        {
            _value = value;
            _date = date;
        }

        public double Value
        {
            get { return _value; }
        }

        public DateTime Date
        {
            get { return _date; }
        }
    }
}
