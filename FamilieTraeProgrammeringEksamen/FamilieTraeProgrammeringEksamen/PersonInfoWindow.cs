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

        static Form1 form1 = new Form1();

        public void ShowPersonInfo(int personID) {
            string FirstName = form1.CommandReadQuery($"select FirstName from Members where ID = {personID}");
            string LastName = form1.CommandReadQuery($"select LastName from Members where ID = {personID}");
            string Age = form1.CommandReadQuery($"select Age from Members where ID = {personID}");
            string Address = form1.CommandReadQuery($"select Address from Members where ID = {personID}");
            int PartnerID = Convert.ToInt32(form1.CommandReadQuery($"select PartnerID from Members where ID = {personID}"));
            string Partner = form1.CommandReadQuery($"select FirstName from Members where ID = {PartnerID}");

            NameL.Text = $"{FirstName} {LastName}";
            AgeL.Text = Age;
            AddressL.Text = Address;
            PartnerL.Text = $"{Partner} {LastName}";
        }
    }
}
