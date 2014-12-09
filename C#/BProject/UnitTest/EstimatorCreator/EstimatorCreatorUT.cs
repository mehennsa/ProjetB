//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Services.EstimatorCreator;
//using EstimatorCr = Services.EstimatorCreator;
//using Services;
//using System.Linq;
//using System.Collections.Generic;

//namespace UnitTest.EstimatorCreator
//{
//    [TestClass]
//    public class EstimatorCreatorUT
//    {
//        private const string MockEstimator = "MockEstimator";

//        private static EstimatorCr.IEstimatorCreator _creator;

//        [ClassInitialize]
//        public static void SetUpTest(TestContext context)
//        {
//            _creator = new EstimatorCr.EstimatorCreator();
//            using (DataProjDataContext ctx = new DataProjDataContext())
//            {
//                Services.Estimator newEstimator = new Services.Estimator
//                {
//                    Name = MockEstimator,
//                    Assembly = typeof(MockEstimator).Assembly.GetName().Name,
//                    Full_Name = typeof(MockEstimator).FullName
//                };
//                ctx.Estimator.InsertOnSubmit(newEstimator);
//                ctx.SubmitChanges();
//            }
//        }


//        [ClassCleanup]
//        public static void TeardDownTest()
//        {
//            using (DataProjDataContext ctx = new DataProjDataContext())
//            {
//                var mock = (from e in ctx.Estimators
//                             where e.Name == MockEstimator
//                             select e).SingleOrDefault();
//                ctx.Estimators.DeleteOnSubmit(mock);
//                ctx.SubmitChanges();
//            }

//        }

//        [TestMethod]
//        public void CreateExistingEstimator()
//        {
//            var estimator = _creator.CreateEstimator(MockEstimator);

//            Assert.IsNotNull(estimator);

//            Assert.AreEqual(typeof(MockEstimator), estimator.GetType());
//        }

//        [TestMethod]
//        public void CreateNonExistingEstimator()
//        {
//            string wrongEstimator = "whatever";

//            var estimator = _creator.CreateEstimator(wrongEstimator);

//            Assert.IsNull(estimator);
//        }

//        [TestMethod]
//        public void CreateWithNullString()
//        {
//            var estimator = _creator.CreateEstimator(null);

//            Assert.IsNull(estimator);
//        }

//        [TestMethod]
//        public void CreateEstimatorFromList()
//        {
//            IList<string> names = new List<string>() { MockEstimator, "Whatever" };

//            var estimators = _creator.CreateEstimators(names);

//            Assert.AreEqual(1, estimators.Count);

//            Assert.AreEqual(typeof(MockEstimator), estimators[0].GetType());
//        }

//        [TestMethod]
//        public void CreateEstimatorFromNullList()
//        {
//            var estimators = _creator.CreateEstimators(null);

//            Assert.IsNotNull(estimators);

//            Assert.AreEqual(0, estimators.Count);
//        }

//    }
//}
