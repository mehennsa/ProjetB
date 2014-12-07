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
        void Compute(IAsset asset, string estimatorName);

        void Compute(IAsset asset, IList<string> estimatorNames);

        void GetLastMarketData(IAsset asset);

        void GetEstimatorValues(IAsset asset, string estimatorName, IList<DateTime> dates);

        void RefreshAsset(IAsset asset);
    }
}
