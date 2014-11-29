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
        public Estimator(double value, DateTime date) : base(value, date) { }
     
        // Fonction de calcul du cours. La construction du cours se fait à l'intérieur.
        // curve: ensemble des cours nécessaires (!! ou non?  !!) pour construire le cours.
        public abstract void Compute(Curve curve) ;
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

        public override void Compute(Curve curve)
        {

        }

        public override object Clone()
        {
            return new MA(this.Value, this.Date, this.Term);
        }
    }
}
