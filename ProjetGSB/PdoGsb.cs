using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.util;

namespace ProjetGSB
{
    class PdoGsb
    {
        /// <summary>
        /// 
        /// </summary>
        private string ChaineConnexion { get; set; }
        private static readonly string server = "localhost";
        private static readonly string user = "root";
        private static readonly string mdp = "";
        private static readonly string database = "gsb_frais";
        private static PdoGsb instancePdoGsb = null;
        private MySqlConnection connexion;


        /// <summary>
        /// Constructeur privé, sert à créer l'instance Pdo utilisé par les autres méthodes de la classe
        /// </summary>
        private PdoGsb()
        {
            this.InitConnexion();
        }

        /// <summary>
        /// Méthode pour initialiser une connexion
        /// </summary>
        private void InitConnexion()
        {
            MySqlConnectionStringBuilder connectionString = new MySqlConnectionStringBuilder
            {
                Server = server,
                UserID = user,
                Password = mdp,
                Database = database
            };

            string chaineConnexion = connectionString.ToString();
            this.connexion = new MySqlConnection(chaineConnexion);
        }

        /// <summary>
        /// Destructeur de la classe Pdo
        /// </summary>
        public void PdoDestructeur()
        {
            this.ChaineConnexion = null;
        }

        /// <summary>
        /// (singleton) méthode qui verifie si une instance existe déjà, si ce n'est pas le cas elle en crée une
        /// </summary>
        /// <returns>retourne l'unique instance PdoGsb</returns>
        public static PdoGsb GetInstancePdoGsb()
        {
            if (instancePdoGsb == null)
            {
                instancePdoGsb = new PdoGsb();
            }
            return instancePdoGsb;
        }

        /// <summary>
        /// méthode qui ouvre une connexion mysql
        /// </summary>
        public void OpenMySqlConnexion()
        {
            try
            {
                this.connexion.Open();
            }

            catch
            (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        Console.WriteLine("Identifiant/Mot de passe invalide");
                        break;
                }
            }
        }

        

        /// <summary>
        /// Méthode qui execute une requete Update, Delete ou Insert
        /// </summary>
        /// <param name="requete"></param>
        public void ExecuteRequeteAdministration(string requete) 
        {
            //ouvre la connexion
            OpenMySqlConnexion();

            //tente execute la requete
            try
            {
                using MySqlCommand commande = this.connexion.CreateCommand();
                commande.CommandText = requete;

                commande.ExecuteNonQuery();

            }
            catch
            {
                
            }

            //ferme la connexion
            this.connexion.Close();

        }

        /// <summary>
        /// méthode qui éxécute une requète SQL de type select
        /// </summary>
        /// <param name="requete">chaine de requete de type SQL</param>
        /// <returns>une liste d'ojets des éléments de la requête</returns>
        public List<object[]> ExecuteSelect(string requete)
        {
            // crée une liste d'ojbet
           List<object[]> result = new List<object[]>();

            //ouvre la connexion mySql
            OpenMySqlConnexion();

            try
            {
                //execute la requête SELECT
                var commande = new MySqlCommand(requete, connexion);
                using (MySqlDataReader lignesResult = commande.ExecuteReader())
                {
                    while (lignesResult.Read())

                    {
                        //crée un tableau d'objets de la taille du nombre de colones/champs de la requête
                        object[] ligne = new object[lignesResult.FieldCount];
                        // remplis le tableau
                        lignesResult.GetValues(ligne);

                        // ajoute la ligne à la liste d'objets
                        result.Add(ligne);
                    }
                }
                //ferme la connexion
                this.connexion.Close();

                return result;
            }
            catch (MySqlException)
            {
                //ferme la connexion
                this.connexion.Close();

                return result;
            }
        }


        /// <summary>
        /// méthode retourne la ligne d'une liste d'objet dont l'index est passé en paramètre
        /// </summary>
        /// <param name="list"></param>
        public object[] GetUneLigne(List<object[]> uneListe, int index)
        {
            //crée un tableau d'objets de la taille du nombre de colones/champs de la requête
            return uneListe[index];
        }

        

        public object GetUnChamps(object[]champs, int index)
        {
            return champs[index];
        }
     
        
        public object GetUnChampsduneLigne(List<object[]> uneliste, int indexListe, int indexChamps)
        {
            return GetUnChamps(GetUneLigne(uneliste, indexListe), indexChamps);
        }

    }
}
