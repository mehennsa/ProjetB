using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Services.EstimatorCreator;
using Services.MarketDataProvider;
using Services.EstimatorFeeder;
using Engine;
using Tools;

namespace Services.GlobalServices
{
    public class GlobalServices : IGlobalServices
    {

        #region Services

        private IMarketDataProvider _provider;

        private IEstimatorFeeder _feeder;

        private IEstimatorCreator _creator;

        #endregion

        #region Constructor

        public GlobalServices()
        {
            _provider = new MarketDataProvider.MarketDataProvider();

            _feeder = new EstimatorFeeder.EstimatorFeeder();

            _creator = new EstimatorCreator.EstimatorCreator();
        }

        public GlobalServices(IMarketDataProvider provider, IEstimatorFeeder feeder, IEstimatorCreator creator)
        {
            _provider = provider;

            _feeder = feeder;

            _creator = creator;
        }

        #endregion

        #region IGlobalServices

        public void Compute(IAsset asset, string estimatorName)
        {
            // Estimator Creation
            Engine.Estimator estimator = _creator.CreateEstimator(estimatorName);

            if (estimator != null)
            {
                //estimator.Compute()
            }
        }

        public void Compute(IAsset asset, IList<string> estimatorNames)
        {
            throw new NotImplementedException();
        }

        public void GetLastMarketData(IAsset asset)
        {
            throw new NotImplementedException();
        }

        public void GetEstimatorValues(IAsset asset, string estimatorName, IList<DateTime> dates)
        {
            throw new NotImplementedException();
        }

        public void RefreshAsset(IAsset asset)
        {
            bool hasToBeRefreshed = false;
            List<DateTime> dates = new List<DateTime>();
            DateTime LastRecordedDate = DateTime.Today;
            foreach (var item in asset.Curves.Keys)
            {
                if (!asset.Curves[item].IsUpToDate)
                {
                    hasToBeRefreshed = true;
                    LastRecordedDate = asset.Curves[item].Quotes.Max((q) => q.Value.Date);
                    break;
                }

                if (hasToBeRefreshed)
                {
                    FillWithWorkingDays(LastRecordedDate, DateTime.Today, dates);
                   // Dictionary<IQuote, Curve> infos = _provider.getLastMarketData(asset.Name, dates);
                }
            }
        }

        #endregion

        #region Private Methods

        private void FillWithWorkingDays(DateTime startDate, DateTime endDate, IList<DateTime> datesToRecord)
        {
            DateTime newDate = startDate;
            while (newDate < endDate)
            {
                datesToRecord.Add(newDate.AddWorkDays(1));
            }
        }

        #endregion
    }
}
