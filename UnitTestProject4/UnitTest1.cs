using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meassurement.Controllers;
using System;
using Meassurement;
using System.Collections.Generic;

namespace UnitTestProject4
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AmountOfDataRows()
        {
           MeassurementsController mc = new MeassurementsController();

            IList<Meassurement> result = mc.GetAllMeassurements();

            var counted = result.Count;

            Assert.AreEqual(5, counted);
        }

        [TestMethod]
        public void OneIdCorrect()
        {
            MeassurementsController res = GetCustomersAsyncid(1).Result;

            Thread.Sleep(1000);

            var ress = res.Pressure;

            Assert.AreEqual(20, ress);
        }
    }
}