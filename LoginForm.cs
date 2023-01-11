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

namespace Appointment_App
{
    public partial class LoginForm : Form
    {
        private CultureInfo CI { get; set; }
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

                MessageBox.Show($"{locrm.GetString("hello", CI)} {Logic.CurrentUserName}\n\n {locrm.GetString("success", CI)}");
                MainForm mainForm = new MainForm();
                Logic.CheckLoginAppointment(loginTime);
                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show(locrm.GetString("incorrectMatch", CI));
            }
        }

        private CultureInfo DetectLanguage()
        {
            if (CultureInfo.CurrentCulture.Name == "es-ES")
            {
                CI = new CultureInfo("es-ES");
                loginLabel.Text = locrm.GetString("loginLabel", CI);
                userLabel.Text = locrm.GetString("userLabel", CI);
                passwordLabel.Text = locrm.GetString("passwordLabel", CI);
                LoginButton.Text = locrm.GetString("LoginButton", CI);
                return CI;
            }
            if (CultureInfo.CurrentCulture.Name == "en-EN")
            {
                CI = new CultureInfo("en-EN");
                loginLabel.Text = locrm.GetString("loginLabel", CI);
                userLabel.Text = locrm.GetString("userLabel", CI);
                passwordLabel.Text = locrm.GetString("passwordLabel", CI);
                LoginButton.Text = locrm.GetString("LoginButton", CI);
                return CI;
            }
            return CI;
        }
    }
}
