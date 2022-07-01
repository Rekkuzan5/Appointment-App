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

        public bool VerifyUser()
        {
            string loginUserName = userNameLoginTextBox.Text;
            string loginPassword = passwordLoginTextBox.Text;

            MySqlConnection c = DBConnection.Conn;
            //c.Open();

            string sqlstring = "SELECT * FROM user;";
            MySqlCommand cmd = new MySqlCommand(sqlstring, c);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            string user = rdr["userName"].ToString();
            string password = rdr["userName"].ToString();
            textBox1.Text = $"User: {user} n/ Password: {password}";

            if (loginUserName == user && loginPassword == password)
            {
                //MessageBox.Show("Successful Login!");
                return true;
            }
            else
            {
                //MessageBox.Show("Login Unsuccessful");
                return false;
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            //MySqlConnection c = DBConnection.Conn;

            //if(c.State == ConnectionState.Open)
            //{
            //    MessageBox.Show("Connection Open!");
            //}

            if(!VerifyUser())
            {
                MessageBox.Show("Login Unsuccessful");
            }
        }

    }
}
