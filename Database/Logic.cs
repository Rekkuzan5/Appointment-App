using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Appointment_App.Database
{
    class Logic
    {
        public static int CurrentUserID { get; set; }
        public static string CurrentUserName { get; set; }

        public static int VerifyUser(string user, string password)
        {
            string connection = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            string sqlstring = $"SELECT userID, userName FROM user WHERE userName = '{user}' AND password = '{password}'";
            MySqlCommand cmd = new MySqlCommand(sqlstring, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Read();
                CurrentUserID = Convert.ToInt32(rdr[0]);
                CurrentUserName = Convert.ToString(rdr[1]);
                MessageBox.Show($"UserID: {CurrentUserID} User: {user} Password: {password}");
                rdr.Close();
                conn.Close();
                return CurrentUserID;
            }
            MessageBox.Show($"User: {CurrentUserName} Password: {password}");
            conn.Close();
            return 0;
        }

    }
}
