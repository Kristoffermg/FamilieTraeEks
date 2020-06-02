using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FamilieTraeProgrammeringEksamen {
    public partial class PersonInfoWindow : Form {
        public PersonInfoWindow() {
            InitializeComponent();
        }
        MySqlConnection sqlCon = new MySqlConnection("Data Source=80.167.72.28,3306;Initial Catalog=FamilieTræ;Persist Security Info=true;User ID=Kristoffer;password=12345678;");
        MySqlCommand sqlCmd;

        static DesignerWindow MainForm = new DesignerWindow();

        public void ShowPersonInfo(int personID) {
            string FirstName = "", LastName = "", Age = "", City = "", Address = "", Partner = "", YearBorn = "", DateBorn = "", YearDeath = "", DateDeath = "";
            int HasPartner = 0, PartnerID = 0, IsDead = 0;
            sqlCon.Open();
            sqlCmd = new MySqlCommand($"select FirstName, LastName, Age, City, Address, IsMarried, YearBorn, DateBorn, IsDead from Members where ID = {personID}", sqlCon);
            MySqlDataReader da = sqlCmd.ExecuteReader();
            while (da.Read()) {
                FirstName = da.GetValue(0).ToString();
                LastName = da.GetValue(1).ToString();
                Age = da.GetValue(2).ToString();
                City = da.GetValue(3).ToString();
                Address = da.GetValue(4).ToString();
                HasPartner = (int)da.GetValue(5);
                YearBorn = da.GetValue(6).ToString();
                DateBorn = da.GetValue(7).ToString();
                IsDead = (int)da.GetValue(8);
            }
            sqlCon.Close();

            NameL.Text = $"{FirstName} {LastName}";
            AgeL.Text = Age;
            AddressL.Text = Address;
            CityL.Text = City;
            // Personen har en partner
            if (HasPartner == 1) {
                PartnerID = Convert.ToInt32(MainForm.CommandReadQuery($"select PartnerID from Members where ID = {personID}"));
                Partner = MainForm.CommandReadQuery($"select FirstName from Members where ID = {PartnerID}");
                PartnerL.Text = $"{Partner} {LastName}";
            }
            else {
                PartnerL.Text = "No partner";
            }
            BirthL.Text = $"{DateBorn} {YearBorn}";
            // Personen er død
            if(IsDead == 1) {
                YearDeath = MainForm.CommandReadQuery($"select YearDeath from Members where ID = {personID}");
                DateDeath = MainForm.CommandReadQuery($"select DateDeath from Members where ID = {personID}");
                DeathDateL.Text = $"{DateDeath} {YearDeath}";
            }
            else {
                DeathDateL.Visible = false;
                label7.Visible = false;
            }
        }
    }
}
