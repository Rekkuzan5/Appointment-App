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
        private StreamWriter logFile;
        string path = "logins.txt";


    public LoginForm()
        {
            InitializeComponent();
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (Logic.VerifyUser(userNameLoginTextBox.Text, passwordLoginTextBox.Text) != 0)
            {
                using (FileStream fs = File.Create(path))
                {
                    AddText(fs, $"{Logic.CurrentUserName} logged in at {Logic.Now}");
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
