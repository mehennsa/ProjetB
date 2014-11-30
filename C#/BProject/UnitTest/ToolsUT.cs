using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools;

namespace UnitTest
{
    [TestClass]
    public class ToolsUT
    {
        [TestMethod]
        public void UTAddWorkingDays()
        {
            DateTime date = new DateTime(2013, 12, 31);

            // 01/01/2014 férié
            Assert.IsTrue(date.AddWorkDays(1).Equals(new DateTime(2014, 01, 02)));
            Assert.IsFalse(date.AddWorkDays(1).Equals(new DateTime(2014, 01, 01)));

            Assert.IsTrue(date.AddWorkDays(-1).Equals(new DateTime(2013, 12, 30)));

            // 04/01/2014 samedi
            Assert.IsTrue(date.AddWorkDays(5).Equals(new DateTime(2014, 01, 08)));
            Assert.IsFalse(date.AddWorkDays(5).Equals(new DateTime(2014, 01, 04)));
        }
    }
}
