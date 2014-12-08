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

        public abstract void Compute(Dictionary<QuoteType, Curve> curve);
    }

    //
    // Moyenne mobile.
    //
    public class MA : Estimator
    {
        // Période de la MM.
        int _term;

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

        public override void Compute(Dictionary<QuoteType, Curve> curve)
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

        public override void Compute(Dictionary<QuoteType, Curve> curve)
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
}
