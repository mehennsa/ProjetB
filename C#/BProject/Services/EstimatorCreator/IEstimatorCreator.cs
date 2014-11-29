using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EstimatorCreator
{
    public interface IEstimatorCreator
    {
        Engine.Estimator CreateEstimator(string estimatorName);

        IList<Engine.Estimator> CreateEstimators(IList<string> estimatorNames);
    }
}
