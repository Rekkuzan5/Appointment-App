using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appointment_App.Database
{
    class DBConnection
    {
        public static MySqlConnection Conn { get; set; }

        public static void StartConnection()
        {
            try
            {
                string connection = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
                Conn = new MySqlConnection(connection);

                // Open db connection
                Conn.Open();

                MessageBox.Show("Connected to MySQL...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void CloseConnection()
        {
            try
            {
                if (Conn != null)
                {
                    Conn.Close();
                    //MessageBox.Show("Done.");
                }
                Conn = null;
            }
             catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
