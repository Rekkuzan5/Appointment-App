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
            //string connection = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            string query = $"SELECT userID, userName FROM user WHERE userName = '{user}' AND password = '{password}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Read();
                CurrentUserID = Convert.ToInt32(rdr[0]);
                CurrentUserName = Convert.ToString(rdr[1]);
                //MessageBox.Show($"UserID: {CurrentUserID} User: {user} Password: {password}");
                rdr.Close();
                conn.Close();
                return CurrentUserID;
            }
            MessageBox.Show($"User: {CurrentUserName} Password: {password}");
            conn.Close();
            return 0;
        }

        public static DateTime GetDateTime()
        {
            return DateTime.Now.ToUniversalTime();
        }

        // Gets the id from any table.  New customers will not need this since the table auto increments userID for us.
        public static int GetID(string table, string id)
        {
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            string query = $"SELECT max({id}) FROM {table}";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                if (rdr[0] == DBNull.Value)
                {
                    return 0;
                }
                return Convert.ToInt32(rdr[0]);
            }
            return 0;
        }

        // Create Customer function will go here.
        public static int CreateCountry(string country)
        {
            int countryId = GetID("country", "countryId") + 1;
            string username = CurrentUserName;
            //DateTime UTCTime = GetDateTime();
            string utcTime = "UTC_TIMESTAMP";

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            MySqlTransaction transaction = conn.BeginTransaction();

            string query = $"INSERT into country (countryId, country, createDate, createdBy, lastUpdateBy)" +
                $"VALUES ('{countryId}', '{country}', '{utcTime}', '{username}', '{username}')";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();

            return countryId;
        }

        public static int CreateCity(int countryId, string city)
        {
            int cityId = GetID("city", "cityID") + 1;
            string username = CurrentUserName;
            //DateTime UTCTime = GetDateTime();
            string utcTime = "UTC_TIMESTAMP";

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            MySqlTransaction transaction = conn.BeginTransaction();

            string query = $"INSERT into city (cityId, city, countryId, createDate, createdBy, lastUpdateBy)" +
                $"VALUES ('{cityId}', '{city}', '{countryId}', '{utcTime}', '{username}', '{username}')";

            MySqlCommand cmd = new MySqlCommand(query, conn)
            {
                Transaction = transaction
            };
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();

            return countryId;
        }

        public static int CreateAddress(int cityId, string address, string zipCode, string phone)
        {
            int addressId = GetID("address", "addressId") + 1;
            string username = CurrentUserName;
            //DateTime UTCTime = GetDateTime();
            string utcTime = "UTC_TIMESTAMP";

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            MySqlTransaction transaction = conn.BeginTransaction();

            string query = $"INSERT into address (addressId, address, cityId, zipCode, phone, createDate, createdBy, lastUpdateBy)" +
                $"VALUES ('{addressId}', '{address}', '{cityId}', '{zipCode}', '{phone}', '{utcTime}', '{username}', '{username}')";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();

            return addressId;

        }
    }
}
