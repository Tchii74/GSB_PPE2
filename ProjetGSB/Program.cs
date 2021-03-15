using System;
using System.Collections.Generic;
using System.Timers;

namespace ProjetGSB
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        // création d'un timer toutes les 
        readonly static Timer timer = new Timer(2000);

        static public void Run()//Object source, ElapsedEventArgs e
        {
            PdoGsb monPdo = PdoGsb.GetInstancePdoGsb();

            string test = "SELECT id, nom, prenom FROM visiteur";
            LinkedList<object[]>test1 = monPdo.ExecuteSelect(test);


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
            if (GestionDates.Entre(01, 15))
            {
                //passer les fiches de frais du mois n-1 à l'etat "CL"                
                string requete = $"UPDATE fichefrais SET idetat = 'CL' WHERE mois = {datePrecedente}";
                monPdo.ExecuteRequeteAdministration(requete);

            }
            else
            {
                // A partir du 20eme jour du mois,
                if (GestionDates.Entre(20, 31))
                {
                    // mettre les fiches "VA" du mois précedent en "RB"
                    string requete = $"UPDATE fichefrais SET idetat = 'RB' WHERE mois = {datePrecedente} AND idetat = 'VA'";
                    monPdo.ExecuteRequeteAdministration(requete);

                }
                else
                {
                    // on ne fait aucune action
                }
            }
            Console.WriteLine($"ok,{DateTime.Now}");
            Console.ReadLine();
        }


        static public void SetTimer ()
        {
            // méthode run executée quand le temps est écoulé
           // timer.Elapsed += Run;

            // retour à zéro du timer
            timer.AutoReset = true;

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //SetTimer();
            //timer.Enabled = true;

            //Console.WriteLine("fermer l'application");
            //Console.ReadLine();
            Run();

        }


    }
    }

