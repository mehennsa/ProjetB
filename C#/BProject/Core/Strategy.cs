using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    abstract class Strategy
    {
        List<Strategy> _dependencies;

        // Renvoi un entier permettant de décrire la tendance de l'actif (achat, vente, neutre ...)
        public abstract int Compute(IAsset asset, DateTime date);
    }
}
