using System;

namespace ProjetGSB
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            PdoGsb monPdo = PdoGsb.GetInstancePdoGsb();
            // recuperer mois n-1
                var moisPrecedent = GestionDates.GetMoisPrecedent();

            //recupere date entiere du mois precedent au format aaaamm
                string datePrecedente;

                if (moisPrecedent != "12")
                {
                    datePrecedente = $"{DateTime.Now.Year}{moisPrecedent}";
                }
                else
                {
                    datePrecedente = $"{DateTime.Now.Year - 1}{moisPrecedent}";
                }

            //verifie que l'on est bien entre le 01 et le 10 du mois
            if (GestionDates.Entre(01, 10))
            {
                //passer les fiches de frais du mois n-1 à l'etat "CL"                
                string requete = $"UPDATE fichefrais SET idetat = 'CL' WHERE mois = {datePrecedente}";
                monPdo.ExecuteRequeteAdministration(requete);

            }
            else
            {
                // A partir du 20eme jour du mois,
                if (GestionDates.Entre(20,31))
                {
                    // mettre les fiches "VA" du mois précedent en "RB"
                    string requete = $"UPDATE fichefrais SET idetat = 'RB' WHERE mois = {datePrecedente} AND idetat = 'VA'";
                    monPdo.ExecuteRequeteAdministration(requete);

                }
                else
                {
                    //rien
                }
            }
                 

                
            }


        }
    }

