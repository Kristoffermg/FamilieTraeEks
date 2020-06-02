using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace FamilieTraeProgrammeringEksamen {
    public partial class DesignerWindow : Form {
        MySqlConnection sqlCon = new MySqlConnection("Data Source=80.167.72.28,3306;Initial Catalog=FamilieTræ;Persist Security Info=true;User ID=Kristoffer;password=12345678;");
        MySqlCommand sqlCmd;

        public DesignerWindow() {
            InitializeComponent();
            this.Text = "Familiy Tree generator";
            IDSpecification.Minimum = FindMinimumID();
            IDSpecification.Maximum = FindMaximumID();
            numberRange.Text = $"Person ID ({IDSpecification.Minimum}-{IDSpecification.Maximum})";
        }

        int FindMinimumID() {
            return Convert.ToInt32(CommandReadQuery("select min(ID) from Members"));
        }

        int FindMaximumID() {
            return Convert.ToInt32(CommandReadQuery("select max(ID) from Members"));
        }

        private void CreateFamily_Click(object sender, EventArgs e) {
            RefreshEverything();
            GraphicsMain(Convert.ToInt32(IDSpecification.Value));
        }

        public string CommandReadQuery(string query) {
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

        PositionValues pos = new PositionValues();

        // Definerer bitmappet, hvori grafikken indsættes
        Bitmap bmp = new Bitmap(1066, 222);  

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
        int partnerID;
        int kidsNum, kid1ID, kid2ID, kid3ID;
        int main1Kid, main2Kid, main3Kid;
        int mainKidsNum;
        int currentlyDrawingGeneration = 1;

        void RefreshEverything() {
            using (Graphics graphics = Graphics.FromImage(bmp)) {
                graphics.Clear(Color.Transparent);
            }

            // Sætter nogle af variablerne om til deres startværdi, så de forrige værdier ikke ville have en indflydelse på den nye tegning af et familietræ
            pos.CurrentY = 0;
            amountOfKidsBranches = 0;
            currentlyDrawingGeneration = 1;

            // Sletter ID, PosX og PosY værdierne i CurrentIDPos tablet
            sqlCmd = new MySqlCommand("Delete from CurrentIDPos", sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        // Metoden, som styrer hele processen om at tegne grafikken
        void GraphicsMain(int startPersonID) {
            // Finder ud af hvor mange børn som det specificerede ID har
            mainKidsNum = Convert.ToInt32(CommandReadQuery($"select KidsNum from Members where ID = {startPersonID}"));

            ID = startPersonID;

            // Finder centeret af bitmappet (X) og gemmer det
            pos.CurrentX = bmp.Width / 2 - rectangleWidth * 2;
            pos.CurrentY += 15;

            // Tegner den aller første person øverst i familietræet
            DrawRectangleWithName(0);
            DefineKidID(true);
            DrawPartnerIfIDHasOne();
            DrawKidsIfIDHasKids();

            for(int i = 0; i < mainKidsNum + 1; i++) {
                if(i == 1) {
                    ID = main1Kid;
                    pos.CurrentX = pos.Kid1X;
                    pos.CurrentY = pos.Kid1Y;
                    DrawPartnerIfIDHasOne();
                    DrawKidsIfIDHasKids();
                }
                else if(i == 2) {
                    ID = main2Kid;
                    pos.CurrentX = pos.Kid2X;
                    pos.CurrentY = pos.Kid2Y;
                    DrawPartnerIfIDHasOne();
                    DrawKidsIfIDHasKids();
                }
                else if(i == 3) {
                    ID = main3Kid;
                    pos.CurrentX = pos.Kid3X;
                    pos.CurrentY = pos.Kid3Y;
                    DrawPartnerIfIDHasOne();
                    DrawKidsIfIDHasKids();
                }
            }

            // Angiver pictureboxen til at have bitmappet, som er blevet designet på, som billede
            graphicsDisplayBox.Image = bmp;
            PersonBoxPosToBinaryTree('X');
        }

        TreeNode Xtree = new TreeNode();
        TreeNode Ytree = new TreeNode();

        void PersonBoxPosToBinaryTree(char XorY) {
            List<int> values = new List<int>();
            sqlCon.Open();
            sqlCmd = new MySqlCommand($"select Pos{XorY} from CurrentIDPos", sqlCon);
            MySqlDataReader da = sqlCmd.ExecuteReader();
            while (da.Read()) {
                values.Add(Convert.ToInt32(da.GetValue(0)));
            }
            sqlCon.Close();
            values.Sort();
            int[] arr = values.ToArray();
            if(XorY == 'X') {
                Xtree.searchingForX = true;
                Xtree.root = Xtree.ArrToBST(arr, 0, arr.Length - 1);
                PersonBoxPosToBinaryTree('Y');
            }
            else if(XorY == 'Y') {
                Ytree.searchingForX = false;
                Ytree.root = Ytree.ArrToBST(arr, 0, arr.Length - 1);
            }
        }

        void InsertPersonIntoCurrentIDPos(int PosX, int PosY) {
            sqlCmd = new MySqlCommand($"insert into CurrentIDPos values({ID}, {PosX}, {PosY})", sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
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
            }
        }

        // Bruges til at udregne positionen for rektanglen når den specifikke person er gift
        void CalculatePartnerRectanglePosition() {
            /* Før var CurrentX i øverste venstre hjørne på rektanglen. 
             * Herunder gåes der først over på den anden side af rektanglen ved at plusse dens X værdi størrelse til og derefter 32 pixels ud */
            pos.CurrentX += rectangleWidth + 32;
            DrawRectangleWithName(0);
        }

        // Sletter alle værdierne i CurrentIDPos når formen bliver lukket, så man ikke kan starte applikationen efter og stadig få informationsvinduet til at poppe op
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            sqlCmd = new MySqlCommand("Delete from CurrentIDPos", sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        // Indeholder X og Y-værdi for klik 
        public static int Xvalue, Yvalue;

        CreatePeople cPeople = new CreatePeople();
        private void CreateTree_Click(object sender, EventArgs e) {
            cPeople.CreateFamily();
            IDSpecification.Minimum = FindMinimumID();
            IDSpecification.Maximum = FindMaximumID();
            numberRange.Text = $"Person ID ({IDSpecification.Minimum}-{IDSpecification.Maximum})";
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {

            /* Først bliver Y-værdien fundet, da de forskellige generationer har samme Y-værdi og X-værdien altid kan variere
             * Hvis man f.eks. trykker på en persons rektangel i bunden af træet mens der er en anden persons rektangel længere oppe med næsten samme X-værdi, vil BST'et returnere toppens rektangels X-værdi */
            Yvalue = Ytree.Find(e.Y, Ytree.root);
            Xvalue = Xtree.Find(e.X, Xtree.root);

            /* Til hvis konsol slåes til og man vil se værdierne for klikket og hvad der er fundet frem til ud fra klikket
            Console.WriteLine($"Click position X: {e.X} Y: {e.Y}");
            Console.Write($"Found: X: {Xvalue}, Y: {Yvalue}");
            Console.WriteLine();
            */

            // Inden personID bliver defineret, tjekkes der, om der overhovedet er fundet en person der hvor man har klikket vha. Xvalue og Yvalue
            if (Xvalue > 0 && Yvalue > 0) {
                PersonInfoWindow InfoWindow = new PersonInfoWindow();
                // Personen, som vha. Binary search tree er blevet fundet, sendes videre til ShowPersonInfo() i PersonInfoWindow.cs, så personens informationer bliver vist
                int personID = Convert.ToInt32(CommandReadQuery($"select ID from CurrentIDPos where PosX = {Xvalue} and PosY = {Yvalue}"));
                InfoWindow.ShowPersonInfo(personID);
                InfoWindow.Show();
            }
        }

        #region Contains methods that draws the amount of kids that needs to be drawn (Maximum amount = 3)
        void DrawOneKid() {
            // Arkiverer midterpositionen, så den senere kan bruges igen
            pos.ArchivedX = pos.CurrentX;

            // Placerer den nuværende X-position sådan, at den lange streg der går fra forældrene bliver centreret på midten af rektanglens X-værdi
            pos.CurrentX -= rectangleWidth / 2;
            ID = kid1ID;
            DrawRectangleWithName(kid1ID);

            // Hvis barnet, som lige er blevet tegnet, er et barn til de første personer, som er tegnet, gemmes barnets position, så de kan bruges senere til hvis barnet har partner/børn
            // Det samme bliver gjort i DrawTwoKids()
            if(currentlyDrawingGeneration == 2) {
                pos.Kid1X = pos.CurrentX;
                pos.Kid1Y = pos.CurrentY;
            }
        }

        void DrawTwoKids(int kid1, int kid2) {
            
            // Arkiverer den tidligere position (midten af kidsBranch), så den senere kan bruges igen
            pos.ArchivedY = pos.CurrentY;
            pos.ArchivedX = pos.CurrentX;

            // Tegner den lille streg, som går fra kidsBranch stregen og ned til barnets rektangel
            pos.CurrentY += 10;
            pos.CurrentX -= kidsBranch;
            from = new Point(pos.CurrentX, pos.ArchivedY);
            to = new Point(pos.CurrentX, pos.CurrentY);
            DrawLine();

            // Placerer den nuværende X-position sådan, at rektanglen bliver tegnet sådan at den lille streg går ned til centeret af rektanglens X-værdi
            pos.CurrentX -= rectangleWidth / 2;
            ID = kid1;
            DrawRectangleWithName(kid1);
            if(currentlyDrawingGeneration == 2) {
                if(mainKidsNum == 2) {
                    pos.Kid1X = pos.CurrentX;
                    pos.Kid1Y = pos.CurrentY;
                }
                else { // mainKidsNum = 3
                    pos.Kid2X = pos.CurrentX;
                    pos.Kid2Y = pos.CurrentY;
                }
            }

            // Laver den nuværende position om til midten af kidsBranch igen
            pos.CurrentY = pos.ArchivedY;
            pos.CurrentX = pos.ArchivedX;

            // Tegner den lille streg, som går fra kidsBranch stregen og ned til barnets rektangel
            pos.CurrentY += 10;
            pos.CurrentX += kidsBranch;
            from = new Point(pos.CurrentX, pos.ArchivedY);
            to = new Point(pos.CurrentX, pos.CurrentY);
            DrawLine();

            pos.ArchivedX = pos.CurrentX;
            // Placerer den nuværende X-position sådan, at rektanglen bliver tegnet sådan at den lille streg går ned til centeret af rektanglens X-værdi
            pos.CurrentX -= rectangleWidth / 2;
            ID = kid2;
            DrawRectangleWithName(kid2);
            if (currentlyDrawingGeneration == 2) {
                if (mainKidsNum == 2) {
                    pos.Kid2X = pos.CurrentX;
                    pos.Kid2Y = pos.CurrentY;
                }
                else { // mainKidsNum = 3
                    pos.Kid3X = pos.CurrentX;
                    pos.Kid3Y = pos.CurrentY;
                }
            }

        }

        void DrawThreeKids(int kid1, int kid2, int kid3) {
            // Laver stregen ned til DrawOneKid(), som normalvis ikke ville være der, hvis der kun var et barn 
            pos.ArchivedY = pos.CurrentY;
            pos.CurrentY += 10;
            from = new Point(pos.CurrentX, pos.ArchivedY);
            to = new Point(pos.CurrentX, pos.CurrentY);
            DrawLine();

            // DrawOneKid() bruges, da der ved 3 børn alligevel skal være et barn i midten, som DrawOneKid() netop tegner
            DrawOneKid();

            // Laver den næverende position om til midten af kidsBranch igen, da DrawOneKid() har ændret på positionen
            pos.CurrentX = pos.ArchivedX;
            pos.CurrentY = pos.ArchivedY;

            // De 2 sidste børn bliver tegnet i enderne af kidsBranch
            DrawTwoKids(kid2, kid3);

        }
        #endregion

        void DrawRectangleWithName(int kidNumber) {
            // Definerer positionen og størrelsen af rektanglen
            var rectangle = new Rectangle(pos.CurrentX, pos.CurrentY, rectangleWidth, rectangleHeight);

            // Gemmer personen og dens position i CurrentIDPos tablet
            InsertPersonIntoCurrentIDPos(pos.CurrentX, pos.CurrentY);

            // Til at holde styr på om der skal tegnes en forælder eller et barn (0 = forælder, over 0 = barn)
            string name;
            if (kidNumber == 0) {
                name = CommandReadQuery($"select FirstName from Members where ID = {ID}");
            }
            else { // kidNumber er over 0, hvilket betyder, at navnet skal fetches ud fra ID'et i kidNumber
                name = CommandReadQuery($"select FirstName from Members where ID = {kidNumber}");
            }

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
                pos.CurrentY += rectangleHeight / 2;
                from = new Point(pos.CurrentX, pos.CurrentY);

                // Afstanden imellem de 2 rektangler er 32, 32 trækkes fra CurrentX så X positionen kommer over til den anden rektangel
                pos.CurrentX -= 32;
                to = new Point(pos.CurrentX, pos.CurrentY);

                // Tegner stregen
                DrawLine();
            }
            else if(action == "KidLineup") {
                /* Stregen, som går fra midten af "Partner-stregen" og ned derfra */
                // Finder midten mellen linjen, som tilslutter de 2 forældre
                pos.CurrentX += 16;

                from = new Point(pos.CurrentX, pos.CurrentY);

                /* Fra forældrene og ned til rektanglen er længden 40 på Y-aksen. Dog bliver der plusset 10 ekstra til hvis der er mere end et barn når børnene bliver tegnet, så derfor bliver der ved mere end
                 * et barn kun plusset 30 til, så generationerne har det samme Y-koordinat. */
                if (kidsNum == 1) {
                    pos.CurrentY += 40;
                }
                else {
                    pos.CurrentY += 30;
                }

                to = new Point(pos.CurrentX, pos.CurrentY);
                
                DrawLine();

                /* Stregen, som alle børnene hænger på (Kun hvis der er mere end et barn) */
                kidsBranch = 0;
                if(kidsNum > 1) {
                    // Udregner længden af stregen 
                    if (currentlyDrawingGeneration == 1) kidsBranch = kidsNum * 120;
                    else kidsBranch = kidsNum * 95;
                    kidsBranch -= amountOfKidsBranches * 70 + kidsNum * 20;

                    from = new Point(pos.CurrentX - kidsBranch, pos.CurrentY);
                    to = new Point(pos.CurrentX + kidsBranch, pos.CurrentY);

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
