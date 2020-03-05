using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace FamilieTraeProgrammeringEksamen {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            string connString = "Server=192.168.0.22,49170; Database=FamilyTree; UID=sa; PASSWORD=eksamen;";
            try {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connString;
                conn.Open();
                connectionStatus.Text = "Connected to database!";
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
                
        }
    }
}
