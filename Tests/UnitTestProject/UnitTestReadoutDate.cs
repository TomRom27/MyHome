using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyHome.Models;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestReadoutDate
    {
        [TestMethod]
        public void TestMethod_YearMatching()
        {
            // dd.MM.yyyy HH:mm";
            string pattern = "dd.MM.2017 HH:mm";
            DateTime date;
            bool matches;
            
            date = new DateTime(2017, 04, 01, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(true, matches);

            date = new DateTime(2016, 04, 01, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(false, matches);

            date = new DateTime(2018, 04, 01, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(false, matches);
        }

        [TestMethod]
        public void TestMethod_MonthMatching()
        {
            // dd.MM.yyyy HH:mm";
            string pattern = "dd.04.yyyy HH:mm";
            DateTime date;
            bool matches;

            date = new DateTime(2017, 04, 01, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(true, matches);

            date = new DateTime(2016, 04, 01, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(true, matches);

            date = new DateTime(2017, 05, 01, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(false, matches);
        }


        [TestMethod]
        public void TestMethod_DayMatching()
        {
            // dd.MM.yyyy HH:mm";
            string pattern = "01.MM.yyyy HH:mm";
            DateTime date;
            bool matches;

            date = new DateTime(2017, 04, 01, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(true, matches);

            date = new DateTime(2016, 04, 01, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(true, matches);

            date = new DateTime(2017, 05, 11, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(false, matches);
        }


        [TestMethod]
        public void TestMethod_DateMatching()
        {
            // dd.MM.yyyy HH:mm";
            string pattern = "01.04.2017 HH:mm";
            DateTime date;
            bool matches;

            date = new DateTime(2017, 04, 01, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(true, matches);

            date = new DateTime(2016, 04, 01, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(false, matches);
        }


        [TestMethod]
        public void TestMethod_TimeMatching()
        {
            // dd.MM.yyyy HH:mm";
            string pattern = "dd.MM.yyyy 11:00";
            DateTime date;
            bool matches;

            date = new DateTime(2017, 04, 01, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(true, matches);

            date = new DateTime(2016, 04, 01, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(true, matches);

            date = new DateTime(2016, 04, 01, 12, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(false, matches);

            date = new DateTime(2016, 04, 01, 11, 10, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(false, matches);
        }


        [TestMethod]
        public void TestMethod_FullMatching()
        {
            // dd.MM.yyyy HH:mm";
            string pattern = "01.04.2017 11:00";
            DateTime date;
            bool matches;

            date = new DateTime(2017, 04, 01, 11, 0, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(true, matches);

            date = new DateTime(2016, 04, 01, 11, 10, 0);
            matches = date.MatchesReadoutDatePattern(pattern);
            Assert.AreEqual(false, matches);
        }

    }

}
