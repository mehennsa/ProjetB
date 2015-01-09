using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Engine
{
    //
    // Courbe possédant un ensemble de cours. 
    // Les cours doivent être cohérents entre eux (pas de mélange de type de cours).
    //
    public class Curve
    {
        CurveList _quotes;
        // Permet de savoir si le cours le plus récent a été ajouté.
        bool _isUpToDate;

        public Curve()
        {
            _quotes = new CurveList();
            _isUpToDate = false;
        }

        public Curve(CurveList quotes, bool isUpToDate)
        {
            _quotes = quotes;
            _isUpToDate = isUpToDate;
        }

        public CurveList Quotes
        {
            get { return _quotes;  }
        }

        public bool IsUpToDate
        {
            get {
                DateTime max = DateTime.MinValue;
                foreach (DateTime date in _quotes.Keys)
                    if (date > max)
                        max = date;
                
                return (max == DateTime.Now.AddWorkDays(-1));
            }
        }

        /// <summary>
        /// Permet de créer une sous courbe entre la minDate et lastDate
        /// </summary>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        public Curve CreateSubCurve(DateTime lastDate)
        {
            Curve subCurve = new Curve();
            // on récupére la date la plus ancienne

            DateTime minDate = (from q in _quotes
                                select q).Min((q) => q.Key);
            if (!_quotes.ContainsKey(lastDate))
            {
                throw new Exception(String.Format("{0} is not in the curve", lastDate));
            }

            for (DateTime d = minDate; d <= lastDate; d = d.AddWorkDays(1))
            {
                subCurve.Quotes.Add((IQuote)_quotes[d].Clone());
            }

            return subCurve;
        }

        // Concatenate two curve
        // Update the current curve
        public void ConcatenateCurve(Curve otherCurve)
        {
            foreach (var item in otherCurve.Quotes)
            {
                if (!_quotes.ContainsKey(item.Key))
                {
                    _quotes.Add(item.Value.Clone() as IQuote);
                }
            }
        }

    }

    //
    // List définie pour construire une courbe.
    // Permet de contrôler l'insertion des cours.
    //
    public class CurveList : Dictionary<DateTime, IQuote>
    {
        public new void Add(IQuote quote)
        {
            if (Count == 0)
                base.Add(quote.Date, quote);
            else
            {
                var list = this.ToList();

                if (Object.ReferenceEquals(list[0].Value.GetType(), quote.GetType()))
                        base.Add(quote.Date, quote);
                else
                    throw new Exception("Try to add different quote type in a curve");
            }
        }

        // Copy Constructor

        public CurveList(Dictionary<DateTime, IQuote> dico)  : base(dico)
        {

        }

        public CurveList() : base()
        {

        }
    }
}
