using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace Core
{
    public class Equity : IAsset
    {
        string _name;

        Dictionary<QuoteType, Curve> _curves;

        public Equity (string name)
        {
            _curves = new Dictionary<QuoteType, Curve>();
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public Dictionary<QuoteType, Curve> Curves
        {
            get { return _curves; }
            set { _curves = value; }
        }
    }
}
