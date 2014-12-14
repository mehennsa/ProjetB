using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Services.MarketDataProvider;
using Services.EstimatorFeeder;
using Engine;

namespace TestBD2
{
    class Program
    {
        static void Main(string[] args)
        {
            //MarketDataProvider bla = new MarketDataProvider();
            //Curve hello = bla.getLastMarketData("MC.PA", QuoteType.OPEN, new DateTime(2011, 05, 22), new DateTime(2015, 06, 13));
            //bla.RefreshDataMarket();
            //Estimatorf
            //EstimatorFeeder
            MA moyenneMobile = new MA(103.5, new DateTime(2013, 06, 05),10);
            MA moyenneMobile2 = new MA(103.5, new DateTime(2013, 05, 05), 10);
            EstimatorFeeder bla = new EstimatorFeeder();
            bla.RecordValue("MA", "Average mobile",moyenneMobile);
            bla.RecordValue("MA", "Average mobile", moyenneMobile2);

            Curve rebla  = bla.GetEstimatorForTicker("MA","MA","Average mobile",new DateTime(2013,03,05),new DateTime(2013,07,05));
        }
    }
}
