using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;

namespace UnitTest
{
    [TestClass]
    public class EngineUnitTest
    {
        [TestMethod]
        public void TestAddQuotesToCurve()
        {
            Curve curve = new Curve();
            Open open1 = new Open(1, new DateTime());
            curve.Add(open1);
            Close close1 = new Close(1, new DateTime());
            curve.Add(close1);
        }
    }
}
