using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetGSB;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetGSB.Tests
{
    [TestClass()]
    public class GestionDatesTests
    {

        [TestMethod()]
        public void GetMoisPrecedentTest()
        {
            string moisattendu = "02";

            string result = GestionDates.GetMoisPrecedent();

            Assert.AreEqual(result, moisattendu);
        }

        [TestMethod()]
        public void GetMoisPrecedentTest1()
        {
            DateTime date = new DateTime(2021, 10, 11);
            string moisattendu = "09";

            string result = GestionDates.GetMoisPrecedent(date);

            Assert.AreEqual(result, moisattendu);
        }

        [TestMethod()]
        public void GetMoisSuivantTest()
        {
            string moisattendu = "04";

            string result = GestionDates.GetMoisSuivant();

            Assert.AreEqual(result, moisattendu);
        }

        [TestMethod()]
        public void GetMoisSuivantTest1()
        {
            DateTime date = new DateTime(2021, 09, 11);
            string moisattendu = "10";

            string result = GestionDates.GetMoisSuivant(date);

            Assert.AreEqual(result, moisattendu);

        }

        [TestMethod()]
        public void EntreTest()
        {
            int valeur1 = 09;
            int valeur2 = 23;


            bool result = GestionDates.Entre(valeur1,valeur2);

            Assert.IsTrue(result == true);
        }

        [TestMethod()]
        public void EntreTest1()
        {
            int valeur1 = 09;
            int valeur2 = 23;

            DateTime date = new DateTime(2021, 09, 11);

            bool result = GestionDates.Entre(valeur1, valeur2, date);

            Assert.IsTrue(result == true);
        }
    }
}