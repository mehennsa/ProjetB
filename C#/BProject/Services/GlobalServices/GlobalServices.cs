using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Services.EstimatorCreator;

namespace Services.GlobalServices
{
    public class GlobalServices : IGlobalServices
    {

        public void Compute(IAsset asset, Engine.Estimator estimator)
        {
            throw new NotImplementedException();
        }

        public void Compute(IAsset asset, IList<Engine.Estimator> estimators)
        {
            throw new NotImplementedException();
        }

        public void GetLastMarketData(IAsset asset)
        {
            throw new NotImplementedException();
        }

        public void GetEstimatorValues(IAsset asset, Engine.Estimator estimators, IList<DateTime> dates)
        {
            throw new NotImplementedException();
        }

        public void RefreshAsset(IAsset asset)
        {
            throw new NotImplementedException();
        }
    }
}
