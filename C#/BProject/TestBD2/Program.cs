using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.MarketDataProvider;

namespace TestBD2
{
    class Program
    {
        static void Main(string[] args)
        {
            MarketDataProvider bla = new MarketDataProvider();
            bla.RefreshDataMarket();
        }
    }
}
