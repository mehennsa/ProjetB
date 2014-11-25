using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public abstract class Stock : Quote
    {
        public Stock(double value, DateTime date) : base(value, date) { }
    }

    public class Open : Stock
    {
        public Open(double value, DateTime date) : base(value, date) { }
    }

    public class Close : Stock
    {
        public Close(double value, DateTime date) : base(value, date) { }
    }

    public class High : Stock
    {
        public High(double value, DateTime date) : base(value, date) { }
    }

    public class Low : Stock
    {
        public Low(double value, DateTime date) : base(value, date) { }
    }

    public class Volume : Stock
    {
        public Volume(double value, DateTime date) : base(value, date) { }
    }
}
