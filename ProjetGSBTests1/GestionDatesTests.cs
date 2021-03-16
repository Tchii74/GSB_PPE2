using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace ProjetGSB.Tests
{
    /// <summary>
    /// classe de tests qui regroupes les méthodes de tests de la classe GestionDates
    /// </summary>
    [TestClass()]
    public class GestionDatesTests
    {
        /// <summary>
        /// tableau de string représentant les numéros des mois de l'année
        /// </summary>
        readonly string[] moisDeAnnee = new string[]{"01","02","03","04","05","06","07","08","09","10","11","12"};

        /// <summary>
        /// méthode qui test la méthode GetMoisPrecedent de GestionDates
        /// </summary>
        [TestMethod()]
        public void GetMoisPrecedentTest()
        {
            string moisattendu;

            for (int i=0; i<12; i++)
            {
                DateTime date = new DateTime(2021, int.Parse(moisDeAnnee[i]), 11);
                if (moisDeAnnee[i] == "01")
                {
                    moisattendu = "12";
                }
                else
                {
                    moisattendu = moisDeAnnee[i - 1];
                }
                string result = GestionDates.GetMoisPrecedent(date);

                Assert.AreEqual(result, moisattendu);
            }
            
        }


        /// <summary>
        /// méthode qui test la méthode GetMoisSuivant de GestionDates
        /// </summary>
        [TestMethod()]
        public void GetMoisSuivantTest()
        {
            string moisattendu;

            for (int i=0; i<12; i++)
            {
                DateTime date = new DateTime(2021, int.Parse(moisDeAnnee[i]), 11);
                if (moisDeAnnee[i] == "12")
                {
                    moisattendu = "01";
                }
                else
                {
                    moisattendu = moisDeAnnee[i + 1];
                }
                string result = GestionDates.GetMoisSuivant(date);

                Assert.AreEqual(result, moisattendu);
            }
        }


        /// <summary>
        /// méthode qui test la méthode Entre de GestionDates
        /// </summary>
        [TestMethod()]
        public void EntreTest()
        {
            int valeur1 = 09;
            int valeur2 = 23;

            //test si valeur1(09) <= 09 <= valeur2(23)
            DateTime date = new DateTime(2021, 11, 09);
            bool result = GestionDates.Entre(valeur1, valeur2, date);
            Assert.IsTrue(result);

            //test si valeur1(09) <= date(09) <= valeur2(23)
            date = new DateTime(2021, 11, 23);
             result= GestionDates.Entre(valeur1, valeur2, date);
            Assert.IsTrue(result);
            
            //test si valeur1(09) <= date(15) <= valeur2(23)
            date = new DateTime(2021, 11, 15);
            result = GestionDates.Entre(valeur1, valeur2, date);
            Assert.IsTrue(result);

            //test si valeur1(09) <= date(24) <= valeur2(23)
            date = new DateTime(2021, 11, 24);
            result = GestionDates.Entre(valeur1, valeur2, date);
            Assert.IsFalse(result);

            //test si valeur1(09) <= date(05) <= valeur2(23)
            date = new DateTime(2021, 11, 05);
            result = GestionDates.Entre(valeur1, valeur2, date);
            Assert.IsFalse(result);
            

        }
    }
}