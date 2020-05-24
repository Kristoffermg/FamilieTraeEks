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
        //MySqlConnection sqlCon = new MySqlConnection("Data Source=192.168.0.37,3306;Initial Catalog=RPIStation;Persist Security Info=true;User ID=MainPC;password=pwd;");
        MySqlConnection sqlCon = new MySqlConnection("Data Source=195.249.237.86,3306;Initial Catalog=FamilieTræ;Persist Security Info=true;User ID=Kristoffer;password=12345678;");
        MySqlCommand sqlCmd;

        public Form1() {
            InitializeComponent();
        }

        private void CreateFamily_Click(object sender, EventArgs e) {
            RefreshEverything();
            GraphicsMain(Convert.ToInt32(numberOfParentGenerations.Value));

            /*
            if (CommandReadQuery("select ID from Members") == "Database is empty") {
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
            */
        }

        string CommandReadQuery(string query) {
            sqlCon.Open();
            sqlCmd = new MySqlCommand(query, sqlCon);
            MySqlDataReader da = sqlCmd.ExecuteReader();
            while (da.Read()) {
                string result = da.GetValue(0).ToString();
                sqlCon.Close();
                return result;
            }
            sqlCon.Close();
            return "Database is empty";
        }

        private void clearDatabase_Click(object sender, EventArgs e) {
            sqlCmd = new MySqlCommand("Delete from Members", sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            
        }

        PaintSettings paint = new PaintSettings();

        // Systemet følger altid kun en side af forældrene, altså går den ud fra en af forældrene og tegner den anden, der er 3 indekses ved married, marriedwho osv da den anden forælder ikke skal tages i betragtning, derimod er der 4 indekses i Names, da den anden forælders navn stadigvæk skal inkluderes til når den anden forælder skal tegnes.
        private void PictureBox1_Paint(object sender, PaintEventArgs e) {
            /*
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
                if (HasPartner[i] == 1) { // Er gift
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

                        // De forskellige indekses til Names[] herunder ændres på alt efter hvor mange børn der er
                        if (ChildrenAmount[i] == 1 || ChildrenAmount[i] == 3) {
                            // 1 barn
                            int index = i + 2;

                            // 3 børn (tegner det ene barn i midten, hvorefter if statementet herunder tegner de 2 børn i siderne af stregen)
                            int inCaseTheres3Children = 0; // Hvis der er 3 børn, er "ChildrenLine" tegnet, hvilket gør, at denne boks skal tegnes en smule længere nede
                            if(ChildrenAmount[i] == 3) {
                                index++;
                                inCaseTheres3Children = 16;
                                from = new Point(paint.CurrentPosX, paint.CurrentPosY);
                                to = new Point(paint.CurrentPosX, paint.CurrentPosY + 16);
                                e.Graphics.DrawLine(pen, from, to); // Tegner den lille streg, som går ned til midterboksen fra "ChildrenLine"
                            }
                            memberBoxSettings = new Rectangle(paint.CurrentPosX - 35, paint.CurrentPosY + inCaseTheres3Children, boxSizeX, 32);
                            e.Graphics.DrawRectangle(pen, memberBoxSettings);
                            TextRenderer.DrawText(e.Graphics, Names[index], this.Font, memberBoxSettings, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                        }
                        if(ChildrenAmount[i] > 1) {
                            int childrenLineLength = ChildrenAmount[i] * 75;
                            for (int index = 0; index < ChildrenAmount[i]; index++) {

                            }
                            from = new Point(paint.CurrentPosX - childrenLineLength, paint.CurrentPosY);
                            to = new Point(paint.CurrentPosX + childrenLineLength, paint.CurrentPosY);
                            e.Graphics.DrawLine(pen, from, to);

                            paint.CurrentPosX -= childrenLineLength;

                             if(ChildrenAmount[i] == 2 || ChildrenAmount[i] == 3 ||ChildrenAmount[i] == 4) {
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
                                TextRenderer.DrawText(e.Graphics, Names[i + 4], this.Font, memberBoxSettings, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                            }
                            // LIGE PT ER CURRENTPOSX,Y PLACERET PÅ HØJRE SIDE AF childrenLineLength
                             

                        }
                    }
                }
            }
            */
        }

        // Definerer bitmappet, hvori grafikken indsættes
        Bitmap bmp = new Bitmap(900, 450);

        // Definerer størrelsen på rektanglerne, som indeholder navnene på de specifikke personer
        int rectangleWidth = 50;
        int rectangleHeight = 25;

        // To points, som bruges til at tegne streger med. Disse to points bliver defineret og derefter bliver DrawLine() kaldt
        Point from;
        Point to;

        // Stregernes farve og tykkelse defineres
        Pen pen = new Pen(Color.Black, 2);

        // Tekstens font
        Font font = new Font(new FontFamily("Candara Light"), 8);

        // Den lange streg, hvor forældrenes børns rektangler hænger ned fra og en variabel, som bruges til at holde øje med hvor mange kidsBranches der er tegnet
        int kidsBranch, amountOfKidsBranches;
        
        // Variablerne fra databasen, som bruges i udregningerne ift. grafikken
        int ID = 1;
        int isMarried, partnerID;
        int hasPartner; // *************************** Bruge denne variabel i stedet for isMarried?********************
        int kidsNum, kid1ID, kid2ID, kid3ID;


        void RefreshEverything() {
            using (Graphics graphics = Graphics.FromImage(bmp)) {
                graphics.Clear(Color.Transparent);
            }
            paint.CurrentPosY = 0;
            amountOfKidsBranches = 0;
        }

        // Metoden, som styrer hele processen om at tegne grafikken
        void GraphicsMain(int startPersonID) {
            // Tager det første ID ud fra startGenerationen

            ID = startPersonID;

            // Finder centeret af bitmappet (X) og gemmer det
            paint.CurrentPosX = bmp.Width / 2 - rectangleWidth * 2;

            // Tegner den aller første person øverst i familietræet
            DrawRectangleWithName(0);

            // Loop som kører indtil alle generationer er gået igennem
            for (int i = 0; i < 3; i++) {
                // Tegner partner og deres børn, hvis ID'et har en partner og børn/et barn
                DrawPartnerIfIDHasOne();
                DrawKidsIfIDHasKids();
            }

            // Angiver pictureboxen til at have bitmappet, som er blevet designet på, som billede
            pictureBox1.Image = bmp;
        }

        void DrawPartnerIfIDHasOne() {
            // Variabel, som indeholder ID'et på personens partner ID
            partnerID = Convert.ToInt32(CommandReadQuery($"select PartnerID from Members where ID = {ID};"));
            if(partnerID != 0) { // Har en partner (0 = Ingen partner)
                ID = partnerID;
                CalculatePartnerRectanglePosition();
                DrawRectangleWithName(0);
                CalculatePointsForLine("Partner");
            }

            
        }

        void DrawKidsIfIDHasKids() {
            kidsNum = Convert.ToInt32(CommandReadQuery($"select KidsNum from Members where ID = {ID}"));
            if (kidsNum != 0) { // Har børn
                CalculatePointsForLine("KidLineup");

                // Når der bliver tegnet et barn, skal ID'et inkrementeres med 1, hvis barnet har en partner (Skal måske ændres ift. om børnene har partner eller ej?)
                ID++;
            }
        }

        // Bruges til at udregne positionen for rektanglen når den specifikke person er gift
        void CalculatePartnerRectanglePosition() {
            // Gemmer den forrige X position
            paint.ArchivedPosX = paint.CurrentPosX;

            /* Før var CurrentPosX i øverste venstre hjørne på rektanglen. 
             * Herunder gåes der først over på den anden side af rektanglen ved at plusse dens X værdi størrelse til og derefter 32 pixels ud */
            paint.CurrentPosX += rectangleWidth + 32;
            DrawRectangleWithName(0);
        }

        // Denne region indeholder metoder, som bruges til at tegne et specifikt antal børn henad en linje
        #region 
        void DrawOneKid() {
            
            paint.ArchivedPosX = paint.CurrentPosX;
            paint.CurrentPosX -= rectangleWidth / 2;
            DrawRectangleWithName(kid1ID);
            ID = kid1ID;
            DrawPartnerIfIDHasOne();
        }

        void DrawTwoKids(int kid1, int kid2) {
            paint.ArchivedPosY = paint.CurrentPosY;
            paint.CurrentPosY += 10;
            from = new Point(paint.CurrentPosX - kidsBranch, paint.ArchivedPosY);
            to = new Point(paint.CurrentPosX - kidsBranch, paint.CurrentPosY);
            DrawLine();
            paint.ArchivedPosX = paint.CurrentPosX;
            paint.CurrentPosX -= rectangleWidth / 2 - kidsBranch;
            DrawRectangleWithName(kid1);
            ID = kid1;
            DrawPartnerIfIDHasOne();

            paint.CurrentPosX = paint.ArchivedPosX;
            paint.CurrentPosY = paint.ArchivedPosY;

            paint.ArchivedPosY = paint.CurrentPosY;
            paint.CurrentPosY += 10;
            from = new Point(paint.CurrentPosX + kidsBranch, paint.ArchivedPosY);
            to = new Point(paint.CurrentPosX + kidsBranch, paint.CurrentPosY);
            DrawLine();
            paint.ArchivedPosX = paint.CurrentPosX;
            paint.CurrentPosX -= rectangleWidth / 2 + kidsBranch;
            DrawRectangleWithName(kid2);
            ID = kid2;
            DrawPartnerIfIDHasOne();
        }

        void DrawThreeKids(int kid1, int kid2, int kid3) {
            // DrawOneKid() bruges, da der ved 3 børn alligevel skal være et barn i midten, som DrawOneKid() netop tegner
            paint.ArchivedPosY = paint.CurrentPosY;
            paint.CurrentPosY += 10;
            from = new Point(paint.CurrentPosX, paint.ArchivedPosY);
            to = new Point(paint.CurrentPosX, paint.CurrentPosY);
            DrawLine();
            DrawOneKid();
            paint.CurrentPosX = paint.ArchivedPosX;
            paint.CurrentPosY = paint.ArchivedPosY;
            DrawTwoKids(kid2, kid3);

        }
        #endregion

        void DrawRectangleWithName(int kidNumber) {
            // Definerer positionen og størrelsen af rektanglen
            var rectangle = new Rectangle(paint.CurrentPosX, paint.CurrentPosY, rectangleWidth, rectangleHeight);


            // Til at holde styr på om der skal tegnes en forælder eller et barn (0 = forælder, over 0 = barn)
            string name = "";
            if (kidNumber == 0) {
                name = CommandReadQuery($"select FirstName from Members where ID = {ID}");
            }
            else { // kidNumber er over 0, hvilket betyder, at navnet skal fetches ud fra ID'et i kidNumber
                name = CommandReadQuery($"select FirstName from Members where ID = {kidNumber}");
            }

            //Pen pen = new Pen(Color.Black, 2); ***************************************************************** TJEK SYNOPSE ***********************************************!!!

            // Tegner grafik på bitmappet
            using (Graphics graphics = Graphics.FromImage(bmp)) {
                graphics.DrawRectangle(pen, rectangle);
                // Tegner navnet på personen i midten af rektanglen
                TextRenderer.DrawText(graphics, name, font, rectangle, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        void CalculatePointsForLine(string action) {
            if(action == "Partner") {
                // Rektanglernes højde bliver divideret med 2 for at finde midten af rektanglen
                paint.CurrentPosY += rectangleHeight / 2;
                from = new Point(paint.CurrentPosX, paint.CurrentPosY);

                // Afstanden imellem de 2 rektangler er 32, 32 trækkes fra CurrentPosX så X positionen kommer over til den anden rektangel
                paint.CurrentPosX -= 32;
                to = new Point(paint.CurrentPosX, paint.CurrentPosY);

                // Tegner stregen
                DrawLine();
            }
            else if(action == "KidLineup") {
                /* Stregen, som går fra midten af "Partner-stregen" og ned derfra */

                // Finder midten mellen linjen, som tilslutter de 2 forældre
                paint.CurrentPosX += 16;

                from = new Point(paint.CurrentPosX, paint.CurrentPosY);

                paint.CurrentPosY += 40;
                to = new Point(paint.CurrentPosX, paint.CurrentPosY);
                
                DrawLine();

                /* Stregen, som alle børnene hænger på (Kun hvis der er mere end et barn) */
                kidsBranch = 0;
                if(kidsNum > 1) {
                    // Udregner længden af stregen **************************************MANGLER ÆNDRING SOM OGSÅ TAGER I BETRAGTNING OM DE ER GIFT OSV************************************
                    kidsBranch = kidsNum * 120;
                    kidsBranch -= amountOfKidsBranches * 100;

                    from = new Point(paint.CurrentPosX - kidsBranch, paint.CurrentPosY);
                    to = new Point(paint.CurrentPosX + kidsBranch, paint.CurrentPosY);

                    DrawLine();

                    amountOfKidsBranches++;
                }

                // Definerer kid1ID, kid2ID, kid3ID og kid4ID til at have forældrenes børns specifikke ID (Hvis forældrene har et barn bliver kid1ID defineret og resten bliver til 0)
                DefineKidID();

                switch (kidsNum) {
                    case 1:
                        DrawOneKid();
                        break;
                    case 2:
                        DrawTwoKids(kid1ID, kid2ID);
                        break;
                    case 3:
                        DrawThreeKids(kid1ID, kid2ID, kid3ID);
                        break;
                }
                kidsNum = 0;
            }
        }

        void DefineKidID() {
            kid1ID = Convert.ToInt32(CommandReadQuery($"select Kid1ID from Members where ID = {ID}"));
            kid2ID = Convert.ToInt32(CommandReadQuery($"select Kid2ID from Members where ID = {ID}"));
            kid3ID = Convert.ToInt32(CommandReadQuery($"select Kid3ID from Members where ID = {ID}"));
        }

        void DrawLine() {
            using (Graphics graphics = Graphics.FromImage(bmp)) {
                graphics.DrawLine(pen, from, to);
            }
        }
    }
}
