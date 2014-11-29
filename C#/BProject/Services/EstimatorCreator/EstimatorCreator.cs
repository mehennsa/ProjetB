using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace Services.EstimatorCreator
{
    public class EstimatorCreator : IEstimatorCreator
    {

        public Engine.Estimator CreateEstimator(string estimatorName)
        {
            using (DataProjDataContext context = new DataProjDataContext())
            {
                var estimator = (from e in context.Estimator 
                                     where e.Name == estimatorName
                                     select e).SingleOrDefault();
                Engine.Estimator newEstimator = estimator != null ? 
                    EstimatorCreatorHelper.CreateEstimatorFromInfo(estimator.Assembly, estimator.Full_Name)
                    : null;
                return newEstimator;
            }
        }

        public IList<Engine.Estimator> CreateEstimators(IList<string> estimatorNames)
        {
            using (DataProjDataContext context = new DataProjDataContext())
            {
                var dbEstimators = (from e in context.Estimator
                                        where estimatorNames.Contains(e.Name)
                                        select e);
                IList<Engine.Estimator> estimators = new List<Engine.Estimator>();

                foreach (var item in dbEstimators)
                {
                    estimators.Add(EstimatorCreatorHelper.CreateEstimatorFromInfo(item.Assembly, item.Full_Name));
                }

                return estimators;
            }
        }
    }
}
