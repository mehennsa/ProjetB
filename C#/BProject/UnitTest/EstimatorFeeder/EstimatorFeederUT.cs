using System;
using Services;
using Services.EstimatorFeeder;
using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.EstimatorFeederUT
{
    [TestClass]
    public class EstimatorFeederUT
    {
        [TestMethod]
        public void RecordAndRetrieveValueUT()
        {
            DateTime firstDate  = new DateTime(2013, 05, 05);
            DateTime secondDate = new DateTime(2012, 06, 10);
            double firstValue = 103.5;
            double secondValue  = 120;

            //Insertion de deux estimateurs dans la BD
            MA moyenneMobile = new MA(firstValue, firstDate, 10);
            MA moyenneMobile2 = new MA(secondValue, secondDate, 10);
            EstimatorFeeder EstimFeed = new EstimatorFeeder();
            EstimFeed.RecordValue("MA", "Mobile Average", moyenneMobile);
            EstimFeed.RecordValue("MA", "Mobile Average", moyenneMobile2);

            //Récupération des données
            Curve rebla = EstimFeed.GetEstimatorForTicker("MA", "MA", "Mobile Average", secondDate, firstDate);
            
            //Vérification des données
            Assert.IsTrue(rebla.Quotes[firstDate].Value == firstValue);
            Assert.IsTrue(rebla.Quotes[firstDate].Date == firstDate);
            Assert.IsTrue(rebla.Quotes[secondDate].Value == secondValue);
            Assert.IsTrue(rebla.Quotes[secondDate].Date == secondDate);

            //Suppression des valeurs mise en place
            EstimFeed.DeleteValue("MA", "Mobile Average", secondDate, firstDate);
        }
    }
}
