using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public interface IQuote
    {
        double Value { get; set; }
        DateTime Date { get; set; }
    }

    public abstract class Quote: IQuote
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
}
