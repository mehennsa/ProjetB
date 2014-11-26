using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Curve
    {
        private readonly List<IQuote> _quotes;
        bool _isUpToDate;

        public IReadOnlyCollection<IQuote> Quotes { get; private set; }

        public Curve()
        {
            _quotes = new List<IQuote>();
            _isUpToDate = false;
        }

        public Curve(List<IQuote> quotes, bool isUpToDate)
        {
            _quotes = quotes;
            _isUpToDate = isUpToDate;
        }

        public bool IsUpToDate
        {
            get { return _isUpToDate; }
            set { _isUpToDate = value; }
        }

        public void Add(IQuote quote)
        {
            if (_quotes.Count == 0)
                _quotes.Add(quote);
            else
            {
                if (Object.ReferenceEquals(_quotes.GetType(), quote.GetType()))
                    _quotes.Add(quote);
                else
                    throw new Exception("Try to add different quote type in a curve");
            }
        }
    }
}
