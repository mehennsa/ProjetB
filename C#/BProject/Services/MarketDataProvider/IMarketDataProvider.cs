using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIFiMag;
using APIFiMag.Exporter;
using APIFiMag.Datas;
using Engine;
using APIFiMag.Importer;

namespace Services.MarketDataProvider
{
    public interface IMarketDataProvider
    {
        Curve getLastMarketData(string ticker, QuoteType type, DateTime StartDate, DateTime EndDate);

        void RefreshDataMarket();
    }
}