using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace Appointment_App.Database
{
    class Logic
    {
        public static bool VerifyUser(string user, string password)
        {
            //string loginUserName = userNameLoginTextBox.Text;
            //string loginPassword = passwordLoginTextBox.Text;

            MySqlConnection c = DBConnection.Conn;
            c.Open();

            string sqlstring = "SELECT userID, userName FROM user WHERE userName = '{user}' AND password = '{password}'";
            MySqlCommand cmd = new MySqlCommand(sqlstring, c);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Read();
                string username = rdr["userName"].ToString();
                string pass = rdr["userName"].ToString();
                Console.WriteLine($"User: {user} n/ Password: {password}");

                //setCurrentUserId(Convert.ToInt32(rdr[0]));
                //setCurrentUserName(Convert.ToString(rdr[1]));
                rdr.Close();
                c.Close();
                return true;
            }
            else {
                //rdr.Read();
                //string username = rdr["userName"].ToString();
                //string pass = rdr["userName"].ToString();
                //Console.WriteLine($"User: {user} n/ Password: {password}");

                //if (loginUserName == user && loginPassword == password)
                //{
                //    MessageBox.Show("successful login!");
                //    return true;
                //}
                //else
                //{
                //    //MessageBox.Show("Login Unsuccessful");
                return false;
            }
        }

    }
}
