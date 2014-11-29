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
            if (string.IsNullOrEmpty(assembly) || string.IsNullOrEmpty(fullName))
                return null;
            try
            {
                return Activator.CreateInstance(assembly, fullName).Unwrap() as Engine.Estimator;

            }
            catch
            {
                throw new Exception("this estimator is not defined");
            }
        }
    }
}
