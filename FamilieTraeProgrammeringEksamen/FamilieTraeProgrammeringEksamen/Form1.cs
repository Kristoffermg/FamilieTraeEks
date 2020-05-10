using System;
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
                    Console.WriteLine("aaa");
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

        List<string> Names = new List<string>() { "Peter", "Hanne", "Gugu", "Gaga"};

        // Irrelevante for nu, skal kun bruges til information på medlemmet (klik på medlemmet, ny form popper op, information står derinde)
        List<int> DadID = new List<int>() { 0, 0, 1, 1 };
        List<int> MomID = new List<int>() { 0, 0, 2, 2 };
        List<int> MarriedWho = new List<int>() { 2, 0, 0 };
        List<int> ChildrenWho = new List<int>() { 3, 4 };

        List<int> Married = new List<int>() { 1, 0, 0 }; // 1 = yes, 0 = no
        List<int> Children = new List<int>() { 1, 0, 0 };
        List<int> ChildrenAmount = new List<int>() { 2, 0, 0 }; 

        int amountOfMembers = 4 - 3;
        int boxSizeX = 70; 
        // Systemet følger altid kun en side af forældrene, altså går den ud fra en af forældrene og tegner den anden, der er 3 indekses ved married, marriedwho osv da den anden forælder ikke skal tages i betragtning, derimod er der 4 indekses i Names, da den anden forælders navn stadigvæk skal inkluderes til når den anden forælder skal tegnes.
        private void PictureBox1_Paint(object sender, PaintEventArgs e) {
            // Finder midten af pictureboxen og minusser det med det dobbelte af boxSizeX, så de 2 øverste bokse (far og mor) bliver centreret i pictureboxen
            int pictureboxCenter = pictureBox1.Width / 2 - (boxSizeX * 2);
            paint.CurrentPosX = pictureboxCenter;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Pen pen = new Pen(Color.Black, 3);
            Point from;
            Point to;
            for (int i = 0; i < amountOfMembers; i++) {
                var memberBoxSettings = new Rectangle(paint.CurrentPosX, paint.CurrentPosY, boxSizeX, 32);
                e.Graphics.DrawRectangle(pen, memberBoxSettings);
                TextRenderer.DrawText(e.Graphics, Names[i], this.Font, memberBoxSettings, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                if (Married[i] == 1) { // Er gift
                    int fromBox_X = paint.CurrentPosX + boxSizeX; // Enden af boksen (X)
                    int fromBox_Y = paint.CurrentPosY + 16; // Midten af boksen (Y)
                    from = new Point(fromBox_X, fromBox_Y);

                    to = new Point(fromBox_X + 35, fromBox_Y);
                    e.Graphics.DrawLine(pen, from, to);

                    memberBoxSettings = new Rectangle(fromBox_X + 35, fromBox_Y - 16, boxSizeX, 32);
                    e.Graphics.DrawRectangle(pen, memberBoxSettings);
                    // i + 1 ved Names[] da morens navn skal bruges
                    TextRenderer.DrawText(e.Graphics, Names[i + 1], this.Font, memberBoxSettings, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                    paint.CurrentPosX = fromBox_X + 17;
                    paint.CurrentPosY = fromBox_Y;

                    if(Children[i] == 1) {
                        from = new Point(paint.CurrentPosX, paint.CurrentPosY);
                        to = new Point(paint.CurrentPosX, paint.CurrentPosY + 48); // 48 = 16 (ned til enden af boksen) + 32 (det der svarer til en boks's højde)
                        e.Graphics.DrawLine(pen, from, to);
                        paint.CurrentPosY += 48;

                        if (ChildrenAmount[i] == 1) {
                            memberBoxSettings = new Rectangle(paint.CurrentPosX - 35, paint.CurrentPosY, boxSizeX, 32);
                            e.Graphics.DrawRectangle(pen, memberBoxSettings);
                            TextRenderer.DrawText(e.Graphics, Names[i + 2], this.Font, memberBoxSettings, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                        }
                        else if(ChildrenAmount[i] > 1) {
                            int childrenLineLength = ChildrenAmount[i] * 50;
                            from = new Point(paint.CurrentPosX - childrenLineLength, paint.CurrentPosY);
                            to = new Point(paint.CurrentPosX + childrenLineLength, paint.CurrentPosY);
                            e.Graphics.DrawLine(pen, from, to);

                            paint.CurrentPosX -= childrenLineLength;

                             if(ChildrenAmount[i] == 2) {
                                // Går en tand ned, hvor stregen connector med toppen af barnets boks
                                from = new Point(paint.CurrentPosX, paint.CurrentPosY);
                                to = new Point(paint.CurrentPosX, paint.CurrentPosY + 16);
                                e.Graphics.DrawLine(pen, from, to);

                                memberBoxSettings = new Rectangle(paint.CurrentPosX - 35, paint.CurrentPosY + 16, boxSizeX, 32);
                                e.Graphics.DrawRectangle(pen, memberBoxSettings);
                                TextRenderer.DrawText(e.Graphics, Names[i + 2], this.Font, memberBoxSettings, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);


                                paint.CurrentPosX += childrenLineLength * 2;
                                from = new Point(paint.CurrentPosX, paint.CurrentPosY);
                                to = new Point(paint.CurrentPosX, paint.CurrentPosY + 16);
                                e.Graphics.DrawLine(pen, from, to);

                                memberBoxSettings = new Rectangle(paint.CurrentPosX - 35, paint.CurrentPosY + 16, boxSizeX, 32);
                                e.Graphics.DrawRectangle(pen, memberBoxSettings);
                                TextRenderer.DrawText(e.Graphics, Names[i + 3], this.Font, memberBoxSettings, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                            }
                            // LIGE PT ER CURRENTPOSX,Y PLACERET PÅ VENSTRE SIDE AF childrenLineLength
                        }
                    }
                }
                

            }

            for (int i = 0; i < 1; i++) {
                /*
            int RecPosX1 = paint.RecPosX;
            int RecPosY1 = paint.RecPosY;
            var rect1 = new Rectangle(RecPosX1, RecPosY1, 70, 32);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Pen objMyPen1 = new Pen(Color.Black, 3);
            e.Graphics.DrawRectangle(objMyPen1, rect1);
            TextRenderer.DrawText(e.Graphics, Names[0], this.Font, rect1, Color.Black,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            
            for(int i = 0; i < 4; i++) {
                switch(i) {
                    case 1:
                        if(Person1[2] == "Yes") {
                            int RecPosX = paint.RecPosX;
                            int RecPosY = paint.RecPosY;
                            var rect = new Rectangle(RecPosX, RecPosY, 70, 32);
                            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            Pen objMyPen = new Pen(Color.Black, 3);
                            e.Graphics.DrawRectangle(objMyPen, rect);
                            TextRenderer.DrawText(e.Graphics, Parents[0], this.Font, rect, Color.Black,
                                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                            
                        }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
            }
            */

                /*
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
                */
            }
        }
    }
}
