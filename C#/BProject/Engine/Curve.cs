using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Curve
    {
        List<IQuote> _quotes;
        bool _isUpToDate;

        public List<IQuote> Quotes 
        {
            get { return _quotes; }
            set { _quotes = value; }
        }

        public bool IsUpToDate
        {
            get { return _isUpToDate; }
            set { _isUpToDate = value; }
        }
    }
}
