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

        public abstract void Compute(Curve curve);
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

        public override void Compute(Curve curve)
        {
            double sum = 0.0;
            
            for (int i = 0; i < _term; i++)
                sum += curve.Quotes[_date.AddWorkDays(-i)].Value;

            _value = sum;
        }

        public override object Clone()
        {
            return new MA(this.Value, this.Date, this.Term);
        }
    }
}
