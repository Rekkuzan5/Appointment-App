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

namespace Appointment_App
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public static void VerifyUser()
        {
            string connection = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(connection);

                // Open db connection
                conn.Open();

                MessageBox.Show("Connected to MySQL...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (conn != null)
            {
                conn.Close();
                //MessageBox.Show("Done.");
            }
        }


        private void LoginButton_Click(object sender, EventArgs e)
        {

        }

    }
}
