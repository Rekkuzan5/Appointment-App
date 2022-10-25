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
using System.IO;

namespace Appointment_App
{
    public partial class LoginForm : Form
    {
        //private StreamWriter logFile;
        //string path = "logins.txt";


    public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (Logic.VerifyUser(userNameLoginTextBox.Text, passwordLoginTextBox.Text) != 0)
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(Logic.Path), true))
                {
                    outputFile.WriteLine($"***\nUser: {Logic.CurrentUserName}\nlogged in: {Logic.GetDateTime()}\n***");
                }

                MessageBox.Show($"Hello {Logic.CurrentUserName}, Sign-in Successful");
                MainForm mainForm = new MainForm();
                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Login Failed\n\nUsername and Password did not match.");
            }
        }
    }
}
