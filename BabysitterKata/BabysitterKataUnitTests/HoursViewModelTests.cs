using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BabysitterKata.Models;

namespace BabysitterKataUnitTests
{
    [TestClass]
    public class HoursViewModelTests
    {
        [TestMethod]
        public void CalculateNightlyChargeTest()
        {
            // 7 = Time.Midnight.Value
            // 11 is max (4 AM)
            
            HoursViewModel model = new HoursViewModel(0, 2, 3);
            Assert.AreEqual(32, model.NightlyCharge, "Failed PreMidnight test");

            model = new HoursViewModel(1, 1, 4);
            Assert.AreEqual(24, model.NightlyCharge, "Failed Start At Bedtime test");

            model = new HoursViewModel(1, 6, 6);
            Assert.AreEqual(60, model.NightlyCharge, "Failed no bedtime test");

            model = new HoursViewModel(1, 6, 10);
            Assert.AreEqual(116, model.NightlyCharge, "Failed Bedtime= premidnight end = postmidnight test");

            model = new HoursViewModel(1, 10, 11);
            Assert.AreEqual(136, model.NightlyCharge, "Failed Bedtime= postmidnight");

            model = new HoursViewModel(8, 10, 11);
            Assert.AreEqual(48, model.NightlyCharge, "Failed start = postmidnight");
        }
    }
}
