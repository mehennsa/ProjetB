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
    public class MarketDataProvider : IMarketDataProvider
    {
        public DataProjDataContext db;


        public MarketDataProvider()
        {
            db = new DataProjDataContext();
        }

        public Dictionary<IQuote, Curve> getLastMarketData(string ticker, List<DateTime> dates)
        {
            return null;
        }

        public void RefreshDataMarket()
        {
            DateTime LastDat;
            List<String> Actuel=null;
            try
            {
                Actuel = (from x in db.BindingStock
                              select x.Ticker).ToList();
            }
            catch (Exception Bind)
            {
                Console.WriteLine("Veuillez au préalable insérer des Actifs dans la base !");
            }
            //Récupération de la dernière dates
            try
            {
                var SearchLastDate = (from x in db.DataStock orderby x.Date descending select x.Date).ToList();
                LastDat = SearchLastDate[0].Date.AddDays(1);
                //Attention à la prise en compte des jours fériés
                if (LastDat.DayOfWeek == DayOfWeek.Saturday)
                {
                    LastDat = LastDat.AddDays(2);
                }
                else if (LastDat.DayOfWeek == DayOfWeek.Sunday)
                {
                    LastDat = LastDat.AddDays(1);
                }
            }
            catch
            {
                LastDat = new DateTime(2007, 01, 01);
            }


            //Ajout des types de cours voulus
            List<HistoricalColumn> list = new List<HistoricalColumn>();
            list.Add(HistoricalColumn.Close);
            list.Add(HistoricalColumn.High);
            list.Add(HistoricalColumn.Low);
            list.Add(HistoricalColumn.Open);
            list.Add(HistoricalColumn.Volume);
            List<String> symbol = new List<String>();

            //Ajoute les tickers à la liste des symboles 
            for (int i = 0; i < Actuel.Count; i++)
            {
                symbol.Add(Actuel[i]);
            }

            //Import des données
            DataActif actif = new DataActif(symbol, list, LastDat, DateTime.Now);
            actif.ImportData(new ImportYahoo());

            try
            {
                //Ajout des données pour une date fixée
                while (DateTime.Now.CompareTo(LastDat) >= 0)
                {
                    //Ajout pour chaque stock à une date fixée
                    for (int i = 0; i < Actuel.Count; i++)
                    {
                        //Création du Datastock
                        try
                        {
                            var table = (from x in actif.Table.Data where x.Symbol == Actuel[i] && x.Date.Equals(LastDat) select x).ToList();
                            DataStock StockInfo = new DataStock
                            {
                                Date = table[0].Date,
                                Ticker = Actuel[i],
                                Close = table[0].Value.ToString(),
                                High = table[1].Value.ToString(),
                                Low = table[2].Value.ToString(),
                                Open = table[3].Value.ToString(),
                                Volume = table[4].Value.ToString()
                            };
                            db.DataStock.InsertOnSubmit(StockInfo);
                        }
                        catch (Exception e)
                        {

                        }
                    }
                    //Passage à la date suivante
                    LastDat = LastDat.AddDays(1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            //Insertion en base de données
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

