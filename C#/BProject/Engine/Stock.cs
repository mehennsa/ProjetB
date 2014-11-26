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

        public override object Clone()
        {
            return new Open(this.Value, this.Date);
        }
    }

    public class Close : Stock
    {
        public Close(double value, DateTime date) : base(value, date) { }

        public override object Clone()
        {
            return new Close(this.Value, this.Date);
        }
    }

    public class High : Stock
    {
        public High(double value, DateTime date) : base(value, date) { }
        
        public override object Clone()
        {
            return new High(this.Value, this.Date);
        }
    }

    public class Low : Stock
    {
        public Low(double value, DateTime date) : base(value, date) { }
        
        public override object Clone()
        {
            return new Low(this.Value, this.Date);
        }
    }

    public class Volume : Stock
    {
        public Volume(double value, DateTime date) : base(value, date) { }

        public override object Clone()
        {
            return new Volume(this.Value, this.Date);
        }
    }
}
