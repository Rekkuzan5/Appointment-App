using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Windows.Forms;
using Appointment_App.Database;

namespace Appointment_App
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public void VerifyUser()
        {
            string user = userNameLoginTextBox.Text;
            string password = passwordLoginTextBox.Text;

            MySqlConnection c = DBConnection.Conn;
            c.Open();

            string sqlstring = "SELECT * FROM user;";
            MySqlCommand cmd = new MySqlCommand(sqlstring, c);
            MySqlDataReader rdr = cmd.ExecuteReader();
            textBox1.AppendText(rdr.ToString());        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            MySqlConnection c = DBConnection.Conn;

            if(c.State == ConnectionState.Open)
            {
                MessageBox.Show("Connection Open!");
            }
        }

    }
}
