using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace FamilieTraeProgrammeringEksamen {
    public partial class Form1 : Form {
           SqlConnection con = new SqlConnection("Data Source=192.168.0.22,49170;Initial Catalog=FamilyTree;Persist Security Info=true;User ID=sa;password=eksamen;");
        SqlCommand cmd;
        public Form1() {
            InitializeComponent();
        }

        private void CreateFamily_Click(object sender, EventArgs e) {
            try {
                con.Open();
                GenerateFamilyMembers();
            }
            catch (Exception error) {
                MessageBox.Show(error.ToString());
            }
        }

        void GenerateFamilyMembers() {
            int parentGenerations = Convert.ToInt32(numberOfParentGenerations);
            int count;
            string firstName = "";
            string middleName = "";
            string lastName = "";
            int Age;
            int dayBorn;
            int monthBorn;
            int yearBorn;
            int dayDeath;
            int monthDeath;
            int yearDeath;
            for (int ID = 0; ID < parentGenerations; ID++) {
                dayBorn = 1;
                monthBorn = 1;
                yearBorn = yearBornCalc();
                dayDeath = 1;
                monthDeath = 1;
                yearDeath = yearDeathCalc(yearBorn);
                Age = dateBorn_dateDeath_ToAge(yearBorn, yearDeath);
                count = 0;
                foreach (string nameOutput in yearBornBasedNameGenerator(yearBorn)) {
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
                cmd = new SqlCommand("insert into Members values('" + ID + "','" + firstName + "','" + middleName + "','" + lastName + "','" + Age + "','" + dayBorn + "','" + monthBorn + "','" + yearBorn + "','" + dayDeath + "','" + monthDeath + "','" + yearDeath + "')", con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            // Kald på metoder, som danner kontaktinformationer og familierelationer mellem ID'er
        }

        IEnumerable<string> yearBornBasedNameGenerator(int yearBorn) {
            string firstName = "";
            yield return firstName;
            string middleName = "";
            yield return middleName;
            string lastName = "";
            yield return lastName;
        }

        int yearBornCalc() {

            return 1;
        }

        int yearDeathCalc(int yearBorn) {

            return 0; // Hvis person ikke er død
        }

        int dateBorn_dateDeath_ToAge(int yearBorn, int yearDeath) {
            return yearDeath - yearBorn;
        }

        int phoneNumberGenerator() {

            return 1;
        }

        string addressGenerator() {

            return "";
        }


    }
}
