using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Engine
{
    //
    // Représente un cours calculé.
    //
    public abstract class Estimator : Quote
    {
        public Estimator(double value, DateTime date) : base(value, date) {}
        public Estimator(DateTime date) : base(0.0, date) {}

        public Estimator() { }

        public abstract void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.NONE);
    }

    //
    // Moyenne mobile.
    //
    public class MA : Estimator
    {
        // Période de la MM.
        int _term;

        public MA() { }

        public MA(double value, DateTime date, int term) : base(value, date) 
        {
            _term = term;
        }

        public MA(DateTime date, int term) : base (date)
        {
            _term = term;
        }

        public int Term
        {
            get { return _term; }
        }

        public override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.OPEN)
        {
            Curve open = curve[QuoteType.OPEN];
            if (open == null)
            {
                throw new Exception("No open curve find for MA computation.");
            }

            double sum = 0.0;
            for (int i = 0; i < _term; i++)
                sum += open.Quotes[_date.AddWorkDays(-i)].Value;

            _value = sum/_term;
        }

        public override object Clone()
        {
            return new MA(this.Value, this.Date, this.Term);
        }
    }

    //
    // Moyenne mobile exponentielle.
    //
    public class EMA : Estimator
    {
        // Période de la MM.
        int _term;
        // Lissage de la moyenne mobile
        double _smooth;

        public EMA(double value, DateTime date, int term)
            : base(value, date)
        {
            _term = term;
            _smooth = 2 / (_term + 1);
        }

        public EMA(DateTime date, int term)
            : base(date)
        {
            _term = term;
            _smooth = (double)2/(_term + 1);
        }

        public int Term
        {
            get { return _term; }
        }

        public double Smooth
        {
            get { return _smooth; }
        }

        public override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.OPEN)
        {
            Curve open = curve[QuoteType.OPEN];
            if (open == null)
            {
                throw new Exception("No open curve find for EMA computation.");
            }

            double sum = 0.0;
            for (int i = 0; i < _term; i++)
                sum += open.Quotes[_date.AddWorkDays(-i)].Value * Math.Pow(1-_smooth, i); 

            _value = _smooth * sum;
        }

        public override object Clone()
        {
            return new EMA(this.Value, this.Date, this.Term);
        }
    }

    //
    // Moyenne mobile pondérée.
    //
    public class WMA : Estimator
    {
        // Période de la MM.
        int _term;

        public WMA(double value, DateTime date, int term)
            : base(value, date)
        {
            _term = term;
        }

        public WMA(DateTime date, int term)
            : base(date)
        {
            _term = term;
        }

        public int Term
        {
            get { return _term; }
        }

        public override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.OPEN)
        {
            Curve open = curve[QuoteType.OPEN];
            if (open == null)
            {
                throw new Exception("No open curve find for EMA computation.");
            }

            double sum = 0.0;
            for (int i = 0; i < _term; i++)
                sum += open.Quotes[_date.AddWorkDays(-i)].Value * (_term - i);

            _value = sum/( _term*(_term + 1)/2);
        }

        public override object Clone()
        {
            return new WMA(this.Value, this.Date, this.Term);
        }
    }

    //
    // Moyenne mobile de Hull.
    //
    public class HMA : Estimator
    {
        // Période de la MM.
        int _term;

        public HMA(double value, DateTime date, int term)
            : base(value, date)
        {
            _term = term;
        }

        public HMA(DateTime date, int term)
            : base(date)
        {
            _term = term;
        }

        public int Term
        {
            get { return _term; }
        }

        public override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.OPEN)
        {
            Curve open = curve[QuoteType.OPEN];
            if (open == null)
            {
                throw new Exception("No open curve find for EMA computation.");
            }

            WMA wmaHigh, wmaLow;
            Curve wmaDiff = new Curve();
            DateTime tempDate;
            int sqrtTerm = (int)Math.Round(Math.Sqrt(_term));

            // On calcule toutes les WMA nécessaires
            for (int i = _term; i >= _term - sqrtTerm; i--)
            {
                tempDate = Date.AddWorkDays(i-_term);

                wmaHigh = new WMA(tempDate, i / 2);
                wmaLow = new WMA(tempDate, i);

                wmaHigh.Compute(curve);
                wmaLow.Compute(curve);

                wmaDiff.Quotes.Add(new Open(2 * wmaHigh.Value - wmaLow.Value, tempDate));
            }

            // On lance une WMA de période SQRT(_term) sur les WMA calculés
            WMA wmaHull = new WMA(Date, sqrtTerm);
            Dictionary<QuoteType, Curve> dOpenHull = new Dictionary<QuoteType, Curve>();
            dOpenHull.Add(QuoteType.OPEN, wmaDiff);

            wmaHull.Compute(dOpenHull);

            _value = wmaHull.Value;
        }

        public override object Clone()
        {
            return new HMA(this.Value, this.Date, this.Term);
        }
    }
}
