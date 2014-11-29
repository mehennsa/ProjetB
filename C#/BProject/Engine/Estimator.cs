using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    //
    // Représente un cours calculé.
    //
    public abstract class Estimator : Quote
    {
        public Estimator(double value, DateTime date) : base(value, date) {}
        public Estimator(DateTime date) : base(0.0, date) { }
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

        public int Term
        {
            get { return _term; }
        }

        public static MA Compute(Curve curve)
        {
            return new MA(10, new DateTime(2014, 01, 01), 10);
        }

        public override object Clone()
        {
            return new MA(this.Value, this.Date, this.Term);
        }
    }
}
