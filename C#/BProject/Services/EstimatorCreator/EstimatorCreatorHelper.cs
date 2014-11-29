using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EstimatorCreator
{
    public static class EstimatorCreatorHelper
    {
        public static Engine.Estimator CreateEstimatorFromInfo(string assembly, string fullName)
        {
            return Activator.CreateInstance(assembly, fullName).Unwrap() as Engine.Estimator;
        }
    }
}
