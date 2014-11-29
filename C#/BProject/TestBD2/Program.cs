using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace TestBD2
{
    class Program
    {
        static void Main(string[] args)
        {
            IMarketDataProvider bla = new IMarketDataProvider();
            bla.RefreshDataMarket();
        }
    }
}
