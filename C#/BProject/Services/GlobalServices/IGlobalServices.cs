using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace Services.GlobalServices
{
    public interface IGlobalServices
    {
        void Compute(IAsset asset, Engine.Estimator estimator);

        void Compute(IAsset asset, IList<Engine.Estimator> estimators);

        void GetLastMarketData(IAsset asset);

        void GetEstimatorValues(IAsset asset, Engine.Estimator estimators, IList<DateTime> dates);

        void RefreshAsset(IAsset asset);
    }
}
