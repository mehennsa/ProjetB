using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public abstract class Stock : IQuote
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

    public class Open : Stock
    {
    }

    public class Close : Stock
    {
    }

    public class High : Stock
    {
    }

    public class Low : Stock
    {
    }

    public class Volume : Stock
    {
    }
}
