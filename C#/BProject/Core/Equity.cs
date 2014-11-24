using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace Core
{
    public class Equity
    {
        string _name;

        Dictionary<string, Curve> _stocks;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private Dictionary<string, Curve> Stocks
        {
            get { return _stocks; }
            set { _stocks = value; }
        }
    }
}
