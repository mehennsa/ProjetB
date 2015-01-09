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
        protected int _term;

        public Estimator(int term, double value, DateTime date) : base(value, date) { _term = term; }
        public Estimator(int term, DateTime date) : base(0.0, date) { _term = term;}

        public Estimator() { }

        public abstract void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.NONE);
    }

    //
    // Moyenne mobile.
    //
    public class MA : Estimator
    {
        // Période de la MM.
        //int _term;

        public MA() { }

        public MA(double value, DateTime date, int term) : base(term, value, date) 
        {
        }

        public MA(DateTime date, int term) : base (term, date)
        {
        }

        public int Term
        {
            get { return _term; }
        }

        public override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.OPEN)
        {
            Curve open = curve[QuoteType.CLOSE];
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
        //int _term;
        // Lissage de la moyenne mobile
        double _smooth;

        public EMA(double value, DateTime date, int term)
            : base(term, value, date)
        {
            _smooth = 2 / (_term + 1);
        }

        public EMA(DateTime date, int term)
            : base(term, date)
        {
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
            Curve open = curve[QuoteType.CLOSE];
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
        //int _term;

        public WMA(double value, DateTime date, int term)
            : base(term, value, date)
        {
        }

        public WMA(DateTime date, int term)
            : base(term , date)
        {
        }

        public int Term
        {
            get { return _term; }
        }

        public override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.OPEN)
        {
            Curve open = curve[QuoteType.CLOSE];
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
        //int _term;

        public HMA(double value, DateTime date, int term)
            : base(term ,value, date)
        {
        }

        public HMA(DateTime date, int term)
            : base(term, date)
        {
        }

        public int Term
        {
            get { return _term; }
        }

        public override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.OPEN)
        {
            Curve open = curve[QuoteType.CLOSE];
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

                wmaDiff.Quotes.Add(new Close(2 * wmaHigh.Value - wmaLow.Value, tempDate));
            }

            // On lance une WMA de période SQRT(_term) sur les WMA calculés
            WMA wmaHull = new WMA(Date, sqrtTerm);
            Dictionary<QuoteType, Curve> dOpenHull = new Dictionary<QuoteType, Curve>();
            dOpenHull.Add(QuoteType.CLOSE, wmaDiff);

            wmaHull.Compute(dOpenHull);

            _value = wmaHull.Value;
        }

        public override object Clone()
        {
            return new HMA(this.Value, this.Date, this.Term);
        }
    }

    //Calcul du RSI à partir d'une moyenne mobile simple
    public class RSI : Estimator
    {
        // Période du RSI (Généralement 9 ou 14 jours !).
        //int _term;

        public RSI(double value, DateTime date, int term)
            : base(term, value, date)
        {
        }

        public RSI(DateTime date, int term)
            : base(term, date)
        {
        }

        public int Term
        {
            get { return _term; }
        }

        //Calcul du RSI utilisation des n jours précédents la date d'aujourd'hui
        //Et donc des n-1 derniers gains ou perte journalière
        public override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.CLOSE)
        {
            Curve close = curve[QuoteType.CLOSE];
            if (close == null)
            {
                throw new Exception("No open curve find for RSI computation.");
            }
            double profitAverage = 0, looseAverage = 0, currentDiff = 0;
            int profitNumber = 0, looseNumber = 0;
            for (int i =1; i < _term ; i++)
            {
                currentDiff = close.Quotes[_date.AddWorkDays(-_term + i)].Value - close.Quotes[_date.AddWorkDays(-_term + i - 1)].Value;
                if (currentDiff > 0)
                {
                    profitNumber++; profitAverage += currentDiff;
                }
                else
                {
                    looseNumber++; looseAverage += currentDiff;
                }
            }
            profitAverage = profitNumber != 0 ? profitAverage / profitNumber : 1;
            looseAverage = looseNumber != 0 ? looseAverage / looseNumber : 1;
            _value = 100 - 100 / (1 + Math.Abs((profitAverage / looseAverage)));
        }

        public override object Clone()
        {
            return new RSI(this.Value, this.Date, this.Term);
        }
    }

    //Calcul du ROC
    public class ROC : Estimator
    {
        // Période du ROC (Généralement de quelques jours ~ 10 !).
        //int _term;

        public ROC(double value, DateTime date, int term)
            : base(term, value, date)
        {
        }

        public ROC(DateTime date, int term)
            : base(term, date)
        {
        }

        public int Term
        {
            get { return _term; }
        }

        //Calcul du RSI utilisation des n jours précédents la date d'aujourd'hui
        //Et donc des n-1 derniers gains ou perte journalière
        public override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.CLOSE)
        {
            Curve close = curve[QuoteType.CLOSE];
            if (close == null)
            {
                throw new Exception("No open curve find for ROC computation.");
            }
            _value = 100 * (close.Quotes[_date.AddWorkDays(-1)].Value - close.Quotes[_date.AddWorkDays(-_term)].Value) / close.Quotes[_date.AddWorkDays(-_term)].Value;
        }

        public override object Clone()
        {
            return new ROC(this.Value, this.Date, this.Term);
        }
    }

    //Calcul du uniquement de la ligne de MACD
    //Il faudra ensuite calculé la EMA de cette ligne puis réalisé la différence dans les stratégies
    public class MACDLine : Estimator
    {
        // Période du MACD (Généralement la différence entre la EMA 12 et 26 jours !).
        int _farTerm;
        int _nearTerm;

        public MACDLine(double value, DateTime date, int nearTerm, int farTerm, int term)
            : base(term, value, date)
        {
            _farTerm = farTerm;
            _nearTerm = nearTerm;
        }

        public MACDLine(DateTime date, int nearTerm, int farTerm, int term)
            : base(term, date)
        {
            _nearTerm = nearTerm;
            _farTerm = farTerm;
        }

        public int nearTerm
        {
            get { return _nearTerm; }
        }

        public int farTerm
        {
            get { return _farTerm; }
        }

        //Calcul du MACDLine 
        public override void Compute(Dictionary<QuoteType, Curve> curve, QuoteType quoteType = QuoteType.CLOSE)
        {
            Curve close = curve[QuoteType.CLOSE];
            if (close == null)
            {
                throw new Exception("No open curve find for MACD computation.");
            }
            EMA ema12 = new EMA(_date,12);
            EMA ema26 = new EMA(_date,26);
            ema26.Compute(curve);
            ema12.Compute(curve);
            _value = ema12.Value - ema26.Value; 
        }

        public override object Clone()
        {
            return new MACDLine(this.Value, this.Date, this.nearTerm, this.farTerm, this._term);
        }
    }
}
