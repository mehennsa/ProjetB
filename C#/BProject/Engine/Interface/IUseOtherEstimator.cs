using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Interface
{
    /// <summary>
    /// Pour généraliser le fait qu'un estimateur
    /// utilise un autre estimateur (exemple : %D utilise %K)
    /// </summary>
    public interface IUseOtherEstimator
    {
        Estimator UsedEstimator { get; set; }
    }
}
