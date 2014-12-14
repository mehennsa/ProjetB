using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.EstimatorFeeder
{
    public class EstimatorFeeder : IEstimatorFeeder
    {
        //Attribut permettant la connxion à la base de données 
        public DataProjDataContext db;

        //Constructeur vide permettant la construction de la liaison à la base
         public EstimatorFeeder()
        {
            db = new DataProjDataContext();
        }

        //Permet l'insertion pour un ticker et un estimateur particulier d'une quote
        public void RecordValue(string ticker, string estimatorName, IQuote valueToRecord)
        {
            DataEstimator Estim = new DataEstimator
            {
                Date = valueToRecord.Date,
                Name = estimatorName,
                Ticker = ticker,
                Value = valueToRecord.Value.ToString()
            };
            db.DataEstimator.InsertOnSubmit(Estim);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Impossible d'insérer la valeur courante de l'estimateur !");
            }
        }


        //Permet de supprimer les valeur d'un estimateur sur une plage de date
        public void DeleteValue(string ticker, string estimatorName, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var Actual = (from x in db.DataEstimator where x.Date >= StartDate && x.Date <= EndDate && x.Ticker.Equals(ticker) && x.Name.Equals(estimatorName) orderby x.Date ascending select x).ToList();
                foreach (var elem in Actual)
                {
                    db.DataEstimator.DeleteOnSubmit(elem);
                }
            }
            catch(Exception e)
            {
                throw new Exception("Impossible d'effectuer cette requête à la base !");
            }
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Impossible d'effectuer la suppression !");
            }
        }


        //Retourne sous la forme d'une curve les valeurs d'un estimateurs sur une plage de date
        public Curve GetEstimatorForTicker(string ticker, String type, string estimatorName, DateTime StartDate, DateTime EndDate)
        {
            Curve CurrentCurve = new Curve();
            Object CurrentQuote;
            try
            {
                var Actual = (from x in db.DataEstimator where x.Date >= StartDate && x.Date <= EndDate && x.Ticker.Equals(ticker) && x.Name.Equals(estimatorName) orderby x.Date ascending select x).ToList();
                for (int i = 0; i < Actual.Count; i++)
                {
                    string hello = "Engine." + type +",";
                    CurrentQuote = Activator.CreateInstance(Type.GetType(hello + AssemblyName.GetAssemblyName(@".\Engine.dll")), double.Parse(Actual[i].Value), Actual[i].Date, 20);
                    CurrentCurve.Quotes.Add((Quote)CurrentQuote);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Impossible de trouver le bon estimateur !");
            }


            return CurrentCurve;
        }
    }
}
