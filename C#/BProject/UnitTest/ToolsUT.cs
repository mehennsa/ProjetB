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

        // Test pour savoir si un jour est un jour ouvré.
        [TestMethod]
        public void IsWorkingDayTest()
        {
            // Un samedi
            DateTime saturdayDate = new DateTime(2014, 12, 13);
            // Un dimanche
            DateTime sundayDate = new DateTime(2014, 12, 14);
            // Un jour férié
            DateTime holidayDate = new DateTime(2014, 01, 01);

            // bonne date
            DateTime openDay = new DateTime(2014, 12, 15);

            Assert.AreEqual(false, saturdayDate.IsWorkingDay());
            Assert.AreEqual(false, sundayDate.IsWorkingDay());
            Assert.AreEqual(false, holidayDate.IsWorkingDay());
            Assert.AreEqual(true, openDay.IsWorkingDay());
        }

        [TestMethod]
        public void WorkingDaysBetweenDateTest()
        {
            // a non working day
            DateTime nonWorkingDay = new DateTime(2014, 01, 01);

            // a working day in the past

            DateTime workingDay = new DateTime(2014, 12, 05);

            DateTime today = new DateTime(2014, 12, 15);

            Assert.AreEqual(0, today.WorkingDaysFromBetweenDates(today));
            Assert.AreEqual(0, today.WorkingDaysFromBetweenDates(nonWorkingDay));

            Assert.AreEqual(7, today.WorkingDaysFromBetweenDates(workingDay));
            Assert.AreEqual(7, workingDay.WorkingDaysFromBetweenDates(today));
        }

    }
}
