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
using System.Globalization;

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
                estimator.Compute(asset.Curves);
                _feeder.RecordValue(asset.Name, estimatorName, estimator);
            }
        }

        public void Compute(IAsset asset, IList<string> estimatorNames)
        {
            if (estimatorNames == null || estimatorNames.Count == 0)
            {
                return;
            }
            foreach (var item in estimatorNames)
            {
                Compute(asset, item);
            }
        }

        // TODO : améliorer algo pour mettre à jour les courbes.
        public void GetLastMarketData(IAsset asset)
        {
            
            DateTime endDate = DateTime.Today.AddWorkDays(-1);

            foreach (var key in asset.Curves.Keys)
            {
                Curve currentCurve = asset.Curves[key];
                if (!currentCurve.IsUpToDate)
                {

                    // startDate
                    DateTime startDate = (from q in currentCurve.Quotes.Keys
                                          select q).Max();
                    Curve newCurve = _provider.getLastMarketData(asset.Name, key, startDate, endDate);
                    currentCurve.ConcatenateCurve(newCurve);
                }
            }
        }

        // TODO fin de l'algo
        public void GetEstimatorValues(IAsset asset, string estimatorName, DateTime startDate, DateTime endDate)
        {
            // On vérifie que la courbe est à jour ou non
            // Si non on récupère la courbe via le feeder
            // ON concatène.
        }

        public void RefreshAsset(IAsset asset)
        {
          
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
