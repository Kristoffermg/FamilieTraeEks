using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace FamilieTraeProgrammeringEksamen {
    public partial class Form1 : Form {
        protected MySqlConnection sqlCon = new MySqlConnection("Data Source=195.249.237.86,3306;Initial Catalog=FamilieTræ;Persist Security Info=true;User ID=Admin;password=12345678;");
        protected MySqlCommand sqlCmd;
        Person psn = new Person();

        public Form1() {
            InitializeComponent();
        }

        private void CreateFamily_Click(object sender, EventArgs e) {
            if (CommandQuery("Read", "select ID from Members") == "Database is empty") {
                try {
                    GenerateFamilyMembers();
                }
                catch (Exception error) {
                    MessageBox.Show(error.ToString());
                }
            }
            else {
                MessageBox.Show("Database isn't empty, please clear it before creating a new tree");
            }
        }

        private void clearDatabase_Click(object sender, EventArgs e) {
            sqlCmd = new MySqlCommand("Delete from Members", sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        string CommandQuery(string type, string command) {
            sqlCon.Open();
            sqlCmd = new MySqlCommand(command, sqlCon);
            MySqlDataReader da = sqlCmd.ExecuteReader();
            while (da.Read()) {
                sqlCon.Close();
                return da.GetValue(0).ToString();
            }
            sqlCon.Close();
            return "Database is empty";
        }

        void GenerateFamilyMembers() {
            int parentGenerations = Convert.ToInt32(numberOfParentGenerations.Value);
            int count;
            string firstName = "";
            string middleName = "";
            string lastName = "";
            int Age;
            string dateBorn;
            string dateDeath;
            string gender;
            for (int ID = 0; ID < parentGenerations; ID++) {
                dateBorn = "";
                dateDeath = "";
                gender = "";
                Age = dateBorn_dateDeath_ToAge(dateBorn, dateDeath);
                count = 0;
                foreach (string nameOutput in yearBornBasedNameGenerator(dateBorn)) {
                    switch (count) {
                        case 0:
                            firstName = nameOutput;
                            break;
                        case 1:
                            middleName = nameOutput;
                            break;
                        case 2:
                            lastName = nameOutput;
                            break;
                    }
                    count++;
                }
                sqlCon.Open();
                sqlCmd = new MySqlCommand($"insert into Members values({ID}, {firstName}, {middleName}, {lastName}, {Age}, {dateBorn}, {dateDeath}, {gender})", sqlCon);
                sqlCmd.ExecuteNonQuery();
            }
            sqlCon.Close();
            // Kald på metoder, som danner kontaktinformationer og familierelationer mellem ID'er
        }

        IEnumerable<string> yearBornBasedNameGenerator(string yearBorn) {

            string firstName = "a";
            yield return firstName;
            string middleName = "b";
            yield return middleName;
            string lastName = "c";
            yield return lastName;
        }

        int yearBornCalc() {

            return 1;
        }

        int yearDeathCalc(int yearBorn) {

            return 0; // Hvis person ikke er død
        }

        int dateBorn_dateDeath_ToAge(string yearBorn, string yearDeath) {
            return 1;
        }

        int phoneNumberGenerator() {

            return 1;
        }

        string addressGenerator() {

            return "";
        }


    }
}
