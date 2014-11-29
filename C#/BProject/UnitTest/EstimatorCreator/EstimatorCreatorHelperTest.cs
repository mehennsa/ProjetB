using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ec = Services.EstimatorCreator;

namespace UnitTest.EstimatorCreator
{
    [TestClass]
    public class EstimatorCreatorHelperTest
    {
        private const string EstimatorName = "MockEstimator";

        private const string AssemblyName = "UnitTest";

        private const string FullName = "UnitTest.EstimatorCreator.MockEstimator";

        [TestMethod]
        public void CreateEstimatorValidTest()
        {
            var estimator = Ec.EstimatorCreatorHelper.CreateEstimatorFromInfo(AssemblyName, FullName);

            Assert.IsNotNull(estimator);

            Assert.AreEqual(typeof(MockEstimator), estimator.GetType());
        }

        [TestMethod]
        public void CreateEstimatorWithNullParametersTest()
        {
            var estimatorWithoutAssembly = Ec.EstimatorCreatorHelper.CreateEstimatorFromInfo(null, FullName);

            Assert.IsNull(estimatorWithoutAssembly);

            var estimatorWithoutFullName = Ec.EstimatorCreatorHelper.CreateEstimatorFromInfo(AssemblyName, null);

            Assert.IsNull(estimatorWithoutFullName);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "this estimator is not defined")]
        public void CreateNotExistingEstimator()
        {
            var wrongEstimator = Ec.EstimatorCreatorHelper.CreateEstimatorFromInfo("MyAssembly", "MyFullName");
        }

    }
}
