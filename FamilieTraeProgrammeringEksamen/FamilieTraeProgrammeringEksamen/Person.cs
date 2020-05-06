using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieTraeProgrammeringEksamen {
    class Person {
        protected MySqlConnection sqlCon = new MySqlConnection("Data Source=195.249.237.86,3306;Initial Catalog=FamilieTræ;Persist Security Info=true;User ID=Admin;password=12345678;");
        protected MySqlCommand sqlCmd;

        #region Overload Methods - Find Values Of Individuals

        //Bruger indsendte værdier til at finde enkelte værdier
        public void FindIndividual(string name, string surname, int infoWanted) {
            Console.WriteLine(CommandQuery($"select * from Members where FirstName=@0 and LastName=@1", name, surname, infoWanted));
        }
        public void FindIndividual(int ID, int infoWanted) {
            Console.WriteLine(CommandQuery($"select * from Members where ID=@0", ID, infoWanted));
        }

        //Bruger indsendte værdier til at finde alle værdier på personen
        public void FindAllIndividualDetails(string name, string surname) {
            for (int i = 0; i < 10; i++) {
                Console.WriteLine(CommandQuery($"select * from Members where FirstName=@0 and LastName=@1", name, surname, i));
            }
        }
        public void FindAllIndividualDetails(int ID) {
            for (int i = 0; i < 10; i++) {
                Console.WriteLine(CommandQuery($"select * from Members where ID=@0", ID, i));
            }
        }

        //Søger gennem databasen efter værdier
        string CommandQuery(string command, int ID, int infoWanted) {
            sqlCon.Open();
            sqlCmd = new MySqlCommand(command, sqlCon);
            ///I sqlCmd erstattes @0 med ID for at hjælpe mod sql injections
            ///ID bliver behandlet som en værdi, ikke en kommando til sql
            sqlCmd.Parameters.AddWithValue("@0", ID);
            MySqlDataReader da = sqlCmd.ExecuteReader();
            while (da.Read()) {
                string value = da.GetValue(infoWanted).ToString();
                sqlCon.Close();
                return value;
            }
            sqlCon.Close();
            return "Database is empty";
        }
        string CommandQuery(string command, string name, string surname, int infoWanted) {
            sqlCon.Open();
            sqlCmd = new MySqlCommand(command, sqlCon);
            ///I sqlCmd erstattes @0 med name og @1 med surname for at hjælpe mod sql injections
            ///name og surname bliver behandlet som en værdi, ikke en kommando til sql
            sqlCmd.Parameters.AddWithValue("@0", name);
            sqlCmd.Parameters.AddWithValue("@1", surname);
            MySqlDataReader da = sqlCmd.ExecuteReader();
            while (da.Read()) {
                string value = da.GetValue(infoWanted).ToString();
                sqlCon.Close();
                return value;
            }
            sqlCon.Close();
            return "Database is empty";
        }

        #endregion





    }
}