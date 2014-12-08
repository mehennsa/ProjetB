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

        private const string DB_ERROR_MESSAGE = "Impossible to connect with the database";

        public Engine.Estimator CreateEstimator(string estimatorName)
        {
            if (string.IsNullOrEmpty(estimatorName))
                return null;

            using (DataProjDataContext context = new DataProjDataContext())
            {
                Estimator estimator;

                try
                {
                    estimator = (from e in context.Estimator
                                     where e.Name == estimatorName
                                     select e).SingleOrDefault();
                }
                catch
                {
                    throw new Exception(DB_ERROR_MESSAGE);
                }

                try
                {

                    Engine.Estimator newEstimator = estimator != null ?
                        EstimatorCreatorHelper.CreateEstimatorFromInfo(estimator.Assembly, estimator.Full_Name)
                        : null;
                    return newEstimator;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IList<Engine.Estimator> CreateEstimators(IList<string> estimatorNames)
        {
            if (estimatorNames == null || estimatorNames.Count == 0)
            {
                return new List<Engine.Estimator>();
            }

            using (DataProjDataContext context = new DataProjDataContext())
            {
                IQueryable<Estimator> dbEstimators = null;
                try 
                {
                        dbEstimators = (from e in context.Estimator
                                        where estimatorNames.ToList().Contains(e.Name)
                                        select e);
                } 
                catch 
                {
                    throw new Exception("Impossible to connect with the database");
                }

                IList<Engine.Estimator> estimators = new List<Engine.Estimator>();

                try
                {
                    

                    foreach (var item in dbEstimators)
                    {
                        estimators.Add(EstimatorCreatorHelper.CreateEstimatorFromInfo(item.Assembly, item.Full_Name));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return estimators;
            }
        }
    }
}
