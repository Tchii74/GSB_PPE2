using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

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
            OpenMySqlConnexion();
            //this.connexion.Open();

            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = requete;

            commande.ExecuteNonQuery();

            this.connexion.Close();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requete"></param>
        public Dictionary<string, string> ExecuteSelect (string requete)
        {
            Dictionary<string, string > results = new Dictionary<string, string>();
            this.connexion.Open();

            //using MySqlDataReader lireRequete = lireRequete.ExecuteReader();
            var commande = new MySqlCommand(requete, connexion);
            MySqlDataReader lireRequete = commande.ExecuteReader();

          while (lireRequete.Read())
            {
               
                results.Add((string)lireRequete.GetValue(0), (string)lireRequete.GetValue(1));
               
            }

            connexion.Close();

            return results;
        }
    }
}
