﻿using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace FamilieTraeProgrammeringEksamen {
    public partial class Form1 : Form {
           MySqlConnection sqlCon = new MySqlConnection("Data Source=195.249.237.86,3306;Initial Catalog=FamilieTræ;Persist Security Info=true;User ID=Kristoffer;password=12345678;");
        MySqlCommand sqlCmd;
        public Form1() {
            InitializeComponent();
        }

        private void CreateFamily_Click(object sender, EventArgs e) {
            if(CommandQuery("Read", "select ID from Members") == "Database is empty") {
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

        private void clearDatabase_Click(object sender, EventArgs e) {
            sqlCmd = new MySqlCommand("Delete from Members", sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            
        }

        static PaintSettings paint = new PaintSettings();
        // Eksempel på personer, bliver midlertidigt brugt til tests indtil der kan genereres personer til databasen
        List<string> Parents = new List<string>() { "Peter", "Hanne"}; // Far, mor
        List<string> Children = new List<string>() { "Johanne" }; // Barn

        private void PictureBox1_Paint(object sender, PaintEventArgs e) { // Midlertidig til test af hvordan rektanglerne, navnene og stregerne skal tegnes - Vil blive ændret på senere
            Point p3 = new Point();
            for (int i = 0; i < 2; i++) {
                int RecPosX = paint.RecPosX;
                int RecPosY = paint.RecPosY;
                var rect = new Rectangle(RecPosX, RecPosY, 70, 32);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Pen objMyPen = new Pen(Color.Black, 3);
                e.Graphics.DrawRectangle(objMyPen, rect);
                TextRenderer.DrawText(e.Graphics, Parents[0], this.Font, rect, Color.Black,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                Pen blackPen = new Pen(Color.Black, 3);
                int RecXCenter = RecPosX + 35; // 35: Halvdelen af rektanglens width (70)
                int RecYBottom = RecPosY + 32; 
                Point p1 = new Point(RecXCenter, RecYBottom);
                Point p2 = new Point(RecXCenter, RecYBottom + 20);
                if(i == 1) {
                    e.Graphics.DrawLine(blackPen, p3, p2);
                }
                p3 = p2; // Gemmer det forrige point til stregen, som connecter de to streger 
                e.Graphics.DrawLine(blackPen, p1, p2);
                Parents.RemoveAt(0);
            }

        }
    }
}