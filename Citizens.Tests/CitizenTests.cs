using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Citizens.Tests
{
    [TestClass]
    public class CitizenTests: TestsBase
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithInvalidGender_ThrowsArgumentOutOfRangeException()
        {
            var citizen = new Citizen("Roger", "Pierce", SystemDateTime.Now(), (Gender)2);
        }

        [TestMethod]
        public void Constructor_WithInvalidNameCasing_CorrectsNameToLowerCaseWithCapital()
        {
            var citizen = new Citizen("RoGer", "pIERCE", SystemDateTime.Now(), Gender.Male);
            Assert.AreEqual("Roger", citizen.FirstName);
            Assert.AreEqual("Pierce", citizen.LastName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithDateGreaterThanNow_ThrowsArgumentException()
        {
            var future = TestTodayDate.AddDays(1);
            var citizen = new Citizen("Roger", "Pierce", future, Gender.Male);
        }

        [TestMethod]
        public void Constructor_WithDateTime_StoresDateOnly()
        {
            var dateAndTime = new DateTime(1991, 8, 24, 9, 30, 0);
            var dateOnly = dateAndTime.Date;
            var citizen = new Citizen("Roger", "Pierce", dateAndTime, Gender.Male);

            Assert.AreEqual(dateOnly, citizen.BirthDate);
        }

        // my own tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithEmptyLastName_ThrowsArgumentNullException()
        {
            var citizen = new Citizen("Roger", "", SystemDateTime.Now(), Gender.Male);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullLastName_ThrowsArgumentNullException()
        {
            var citizen = new Citizen("Roger", null, SystemDateTime.Now(), Gender.Male);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithOnlyWhiteSpacesLastName_ThrowsArgumentNullException()
        {
            var citizen = new Citizen("Roger", "     ", SystemDateTime.Now(), Gender.Male);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithEmptyFirstName_ThrowsArgumentNullException()
        {
            var citizen = new Citizen("", "Pierce", SystemDateTime.Now(), Gender.Male);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullFirstName_ThrowsArgumentNullException()
        {
            var citizen = new Citizen(null, "Pierce", SystemDateTime.Now(), Gender.Male);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithOnlyWhiteSpacesFirstName_ThrowsArgumentNullException()
        {
            var citizen = new Citizen("    ", "Pierce", SystemDateTime.Now(), Gender.Male);
        }

        [TestMethod]
        public void Constructor_WithNeedlessWhiteSpacesInName_CorrectsName()
        {
            var citizen = new Citizen(" Roger  ", " Pierce  ", SystemDateTime.Now(), Gender.Male);
            Assert.AreEqual("Roger", citizen.FirstName);
            Assert.AreEqual("Pierce", citizen.LastName);
        }
    }
}
