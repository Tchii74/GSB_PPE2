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
            DateTime date = new DateTime(2021, 10, 11);
            string moisattendu = "09";

            string result = GestionDates.GetMoisPrecedent(date);

            Assert.AreEqual(result, moisattendu);
        }

        

        [TestMethod()]
        public void GetMoisSuivantTest()
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

            DateTime date = new DateTime(2021, 09, 11);
            bool result = GestionDates.Entre(valeur1, valeur2, date);
            Assert.IsTrue(result);

            date = new DateTime(2021, 23, 11);
            result = GestionDates.Entre(valeur1, valeur2, date);
            Assert.IsTrue(result);

            date = new DateTime(2021, 15, 11);
            result = GestionDates.Entre(valeur1, valeur2, date);
            Assert.IsTrue(result);

            date = new DateTime(2021, 24, 11);
            result = GestionDates.Entre(valeur1, valeur2, date);
            Assert.IsFalse(result);
        }
    }
}