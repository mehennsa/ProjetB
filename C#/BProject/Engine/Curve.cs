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
        CurveList<IQuote> _quotes;
        // Permet de savoir si le cours le plus récent a été ajouté.
        bool _isUpToDate;

        public Curve()
        {
            _quotes = new CurveList<IQuote>();
            _isUpToDate = false;
        }

        public Curve(CurveList<IQuote> quotes, bool isUpToDate)
        {
            _quotes = quotes;
            _isUpToDate = isUpToDate;
        }

        public CurveList<IQuote> Quotes
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
    public class CurveList<IQuote> : List<IQuote>
    {
        public new void Add(IQuote quote)
        {
            if (Count == 0)
                base.Add(quote);
            else
            {
                if (Object.ReferenceEquals(this[0].GetType(), quote.GetType()))
                    base.Add(quote);
                else
                    throw new Exception("Try to add different quote type in a curve");
            }
        }
    }
}
