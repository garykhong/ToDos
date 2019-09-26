using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDos.Rules;

namespace ToDos.Tests.Rules
{
    [TestClass]
    public class DateSelectorTest
    {
        private const string CentralStandardTime = TimeZoneID.CentralStandardTime;
        private const string UTC = TimeZoneID.UTC;

        [TestMethod]
        public void GetTodaysDateInAustraliaWithNullParameters_GivesNewDate()
        {
            DateSelector dateSelector = new DateSelector(new DateTime(), null);
            DateTime todaysDateInAustralia = dateSelector.GetTodaysDateInAustralia();
            Assert.AreEqual(1, todaysDateInAustralia.Day);
            Assert.AreEqual(1, todaysDateInAustralia.Month);
            Assert.AreEqual(1, todaysDateInAustralia.Year);
        }

        [TestMethod]
        public void GetTodaysDateInAustraliaWithCentralAmericanTimeZone_GivesAustralianDate()
        {
            DateTime twentiethSeptemberOneAM = new DateTime(2019, 9, 20, 1, 0, 0);
            DateSelector dateSelector = new DateSelector(twentiethSeptemberOneAM, TimeZoneInfo.FindSystemTimeZoneById(CentralStandardTime));
            DateTime twentiethSeptemberFivePM = new DateTime(2019, 9, 20, 17, 0, 0);
            Assert.AreEqual(twentiethSeptemberFivePM, dateSelector.GetTodaysDateInAustralia());
        }

        [TestMethod]
        public void GetTodaysDateInAustraliaWithUTCTimeZone_GivesAustralianDate()
        {
            DateTime twentiethSeptemberOneAM = new DateTime(2019, 9, 20, 1, 0, 0);
            DateSelector dateSelector = new DateSelector(twentiethSeptemberOneAM, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneID.UTC));
            DateTime twentiethSeptemberElevenAM = new DateTime(2019, 9, 20, 11, 0, 0);
            Assert.AreEqual(twentiethSeptemberElevenAM, dateSelector.GetTodaysDateInAustralia());
        }

        [TestMethod]
        public void GetTodaysDateInAustraliaWithChinaStandardTimeZone_GivesAustralianDate()
        {
            DateTime twentiethSeptemberOneAM = new DateTime(2019, 9, 20, 1, 0, 0);
            DateSelector dateSelector = new DateSelector(twentiethSeptemberOneAM, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneID.ChinaStandardTime));
            DateTime twentiethSeptemberThreeAM = new DateTime(2019, 9, 20, 3, 0, 0);
            Assert.AreEqual(twentiethSeptemberThreeAM, dateSelector.GetTodaysDateInAustralia());
        }
    }
}
