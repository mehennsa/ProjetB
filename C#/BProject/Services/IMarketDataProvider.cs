using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIFiMag;
using APIFiMag.Exporter;
using APIFiMag.Datas;
using Engine;

namespace Services
{
    public class IMarketDataProvider
    {
        private DataProjDataContext db;


        public IMarketDataProvider()
        {
            db = new DataProjDataContext();
        }

        public Dictionary<IQuote, Curve> getLastMarketData(string ticker, List<DateTime> dates)
        {
            return null;
        }

        public void RefreshDataMarket()
        {
        }
    }
}
