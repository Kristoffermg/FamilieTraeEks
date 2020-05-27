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

        PaintSettings paint = new PaintSettings();

        // Definerer bitmappet, hvori grafikken indsættes
        Bitmap bmp = new Bitmap(914, 429);

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

        // Bruges til at vide om der gøres brug af det udvalgte ID's partner
        int firstPersonKidAmount;

        // Variablerne fra databasen, som bruges i udregningerne ift. grafikken
        int ID = 1;
        int isMarried, partnerID;
        int hasPartner; // *************************** Bruge denne variabel i stedet for isMarried?********************
        int kidsNum, kid1ID, kid2ID, kid3ID;
        int main1Kid, main2Kid, main3Kid;
        int mainKidsNum;
        int currentlyDrawingGeneration = 1;

        void RefreshEverything() {
            using (Graphics graphics = Graphics.FromImage(bmp)) {
                graphics.Clear(Color.Transparent);
            }
            paint.CurrentPosY = 0;
            amountOfKidsBranches = 0;
            generationsDrawn = 1;
            currentlyDrawingGeneration = 1;
        }

        // Metoden, som styrer hele processen om at tegne grafikken
        void GraphicsMain(int startPersonID) {
            // Tager det første ID ud fra startGenerationen

            mainKidsNum = Convert.ToInt32(CommandReadQuery($"select KidsNum from Members where ID = {startPersonID}")); ;

            ID = startPersonID;

            // Finder centeret af bitmappet (X) og gemmer det
            paint.CurrentPosX = bmp.Width / 2 - rectangleWidth * 2;
            paint.CurrentPosY += 15;

            // Tegner den aller første person øverst i familietræet
            DrawRectangleWithName(0);
            DefineKidID(true);
            DrawPartnerIfIDHasOne();
            DrawKidsIfIDHasKids();

            for(int i = 0; i < mainKidsNum + 1; i++) {
                if(i == 1) {
                    ID = main1Kid;
                    paint.CurrentPosX = paint.Kid1X;
                    paint.CurrentPosY = paint.Kid1Y;
                    DrawPartnerIfIDHasOne();
                    DrawKidsIfIDHasKids();
                }
                else if(i == 2) {
                    ID = main2Kid;
                    paint.CurrentPosX = paint.Kid2X;
                    paint.CurrentPosY = paint.Kid2Y;
                    DrawPartnerIfIDHasOne();
                    DrawKidsIfIDHasKids();
                }
                else if(i == 3) {
                    ID = main3Kid;
                    paint.CurrentPosX = paint.Kid3X;
                    paint.CurrentPosY = paint.Kid3Y;
                    DrawPartnerIfIDHasOne();
                    DrawKidsIfIDHasKids();
                }
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

            }
        }

        // Bruges til at udregne positionen for rektanglen når den specifikke person er gift
        void CalculatePartnerRectanglePosition() {
            /* Før var CurrentPosX i øverste venstre hjørne på rektanglen. 
             * Herunder gåes der først over på den anden side af rektanglen ved at plusse dens X værdi størrelse til og derefter 32 pixels ud */
            paint.CurrentPosX += rectangleWidth + 32;
            DrawRectangleWithName(0);
        }

        int generationsDrawn = 1;

        // Denne region indeholder metoder, som bruges til at tegne et specifikt antal børn henad en linje
        #region 
        void DrawOneKid() {
            paint.ArchivedPosX = paint.CurrentPosX;
            paint.CurrentPosX -= rectangleWidth / 2;
            ID = kid1ID;
            DrawRectangleWithName(kid1ID);
            if(currentlyDrawingGeneration == 2) {
                paint.Kid1X = paint.CurrentPosX;
                paint.Kid1Y = paint.CurrentPosY;
            }
        }

        void DrawTwoKids(int kid1, int kid2) {
            
            paint.ArchivedPosY = paint.CurrentPosY;
            paint.ArchivedPosX = paint.CurrentPosX;
            paint.CurrentPosY += 10;
            paint.CurrentPosX -= kidsBranch;
            from = new Point(paint.CurrentPosX, paint.ArchivedPosY);
            to = new Point(paint.CurrentPosX, paint.CurrentPosY);
            DrawLine();
            paint.CurrentPosX -= rectangleWidth / 2;
            ID = kid1;
            DrawRectangleWithName(kid1);
            if(currentlyDrawingGeneration == 2) {
                if(mainKidsNum == 2) {
                    paint.Kid1X = paint.CurrentPosX;
                    paint.Kid1Y = paint.CurrentPosY;
                }
                else { // mainKidsNum == 3
                    paint.Kid2X = paint.CurrentPosX;
                    paint.Kid2Y = paint.CurrentPosY;
                }
            }

            paint.CurrentPosY = paint.ArchivedPosY;
            paint.CurrentPosX = paint.ArchivedPosX;

            paint.CurrentPosY += 10;

            paint.CurrentPosX += kidsBranch;
            from = new Point(paint.CurrentPosX, paint.ArchivedPosY);
            to = new Point(paint.CurrentPosX, paint.CurrentPosY);
            DrawLine();
            paint.ArchivedPosX = paint.CurrentPosX;
            paint.CurrentPosX -= rectangleWidth / 2;
            ID = kid2;
            DrawRectangleWithName(kid2);
            if (currentlyDrawingGeneration == 2) {
                if (mainKidsNum == 2) {
                    paint.Kid2X = paint.CurrentPosX;
                    paint.Kid2Y = paint.CurrentPosY;
                }
                else { // mainKidsNum == 3
                    paint.Kid3X = paint.CurrentPosX;
                    paint.Kid3Y = paint.CurrentPosY;
                }
            }

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
                // Da der højest må tegnes 3 generationer (Første forældre, deres børn, deres børnebørn) skal positionen for midten af den første kidLineup linje gemmes, så der kan tegnes 1 
                // side af børnebørnene ad gangen
                if(generationsDrawn == 1) {
                    paint.ArchivedPosX2 = paint.CurrentPosX;
                    paint.ArchivedPosY2 = paint.CurrentPosY;
                }

                /* Stregen, som alle børnene hænger på (Kun hvis der er mere end et barn) */
                kidsBranch = 0;
                if(kidsNum > 1) {
                    // Udregner længden af stregen **************************************MANGLER ÆNDRING SOM OGSÅ TAGER I BETRAGTNING OM DE ER GIFT OSV************************************
                    kidsBranch = kidsNum * 100;
                    kidsBranch -= amountOfKidsBranches * 100;

                    from = new Point(paint.CurrentPosX - kidsBranch, paint.CurrentPosY);
                    to = new Point(paint.CurrentPosX + kidsBranch, paint.CurrentPosY);

                    DrawLine();

                    amountOfKidsBranches++;
                }

                // Definerer kid1ID, kid2ID, kid3ID og kid4ID til at have forældrenes børns specifikke ID (Hvis forældrene har et barn bliver kid1ID defineret og resten bliver til 0)
                DefineKidID(false);

                currentlyDrawingGeneration++;
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

        void DefineKidID(bool mainKids) {
            kid1ID = Convert.ToInt32(CommandReadQuery($"select Kid1ID from Members where ID = {ID}"));
            kid2ID = Convert.ToInt32(CommandReadQuery($"select Kid2ID from Members where ID = {ID}"));
            kid3ID = Convert.ToInt32(CommandReadQuery($"select Kid3ID from Members where ID = {ID}"));

            if (mainKids) {
                main1Kid = kid1ID;
                main2Kid = kid2ID;
                main3Kid = kid3ID;
            }
        }

        void DrawLine() {
            using (Graphics graphics = Graphics.FromImage(bmp)) {
                graphics.DrawLine(pen, from, to);
            }
        }
    }
}
