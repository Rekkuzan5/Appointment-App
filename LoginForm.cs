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
using System.Globalization;
using System.Resources;
using System.Reflection;
using Appointment_App.Properties;

namespace Appointment_App
{
    public partial class LoginForm : Form
    {
        //private StreamWriter logFile;
        //string path = "logins.txt";

        CultureInfo ci = new CultureInfo("es-ES");
        ResourceManager locrm = new ResourceManager("Appointment_App.myResources.Resources", typeof(LoginForm).Assembly);

        public LoginForm()
        {
            InitializeComponent();
            DetectLanguage();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (Logic.VerifyUser(userNameLoginTextBox.Text, passwordLoginTextBox.Text) != 0)
            {
                var loginTime = Logic.getDateTime2();
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(Logic.Path), true))
                {
                    outputFile.WriteLine($"***\nUser: {Logic.CurrentUserName}\nlogged in: {loginTime}\n***");
                }

                MessageBox.Show($"{locrm.GetString("hello", ci)} {Logic.CurrentUserName}\n\n {locrm.GetString("success", ci)}");
                MainForm mainForm = new MainForm();
                Logic.CheckLoginAppointment(loginTime);
                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show(locrm.GetString("incorrectMatch", ci));
            }
        }

        private void DetectLanguage()
        {
            //CultureInfo ci = new CultureInfo("es-ES");
            //ResourceManager locrm = new ResourceManager("Appointment_App.myResources.Resources", typeof(LoginForm).Assembly);
            loginLabel.Text = locrm.GetString("loginLabel", ci);
            userLabel.Text = locrm.GetString("userLabel", ci);
            passwordLabel.Text = locrm.GetString("passwordLabel", ci);
            LoginButton.Text = locrm.GetString("LoginButton", ci);


            //if (CultureInfo.CurrentCulture.Name == "es-ES")
            //{
            //    CultureInfo.CurrentCulture = new CultureInfo("es-ES");
            //    CultureInfo ci = new CultureInfo("es-ES");
            //    //loginLabel.Text = locrm.GetString("loginLabel", ci);
            //}
        }
    }
}
