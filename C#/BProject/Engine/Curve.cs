using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            get { return _isUpToDate; }
            set { _isUpToDate = value; }
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
    }
}
