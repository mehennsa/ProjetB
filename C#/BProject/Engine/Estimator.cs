using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public abstract class Estimator : Quote
    {
    }

    public class MA : Estimator
    {
        int _term;

        public int Term
        {
            get { return _term; }
            set { _term = value; }
        }
    }
}
