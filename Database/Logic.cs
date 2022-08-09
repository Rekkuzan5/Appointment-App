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
        public static DateTime Now { get; set; }
        public static DateTime UtcNow { get; }

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
        public static string FormatUTCDateTime(DateTime Now)
        {
            string formatSQLTime = Now.ToString("YYYY-MM-DD hh:mm");
            return formatSQLTime;
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
        public static void CreateCustomer(string name, int addressId, int active, DateTime time, string username)
        {
            string utcTime = FormatUTCDateTime(time);

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            MySqlTransaction transaction = conn.BeginTransaction();

            string query = $"INSERT into customer (customerName, addressId, active, createDate, createdBy, lastUpdateBy)" +
                $"VALUES ('{name}', '{addressId}', {active}, '{utcTime}', '{username}', '{username}')";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();
        }

        // Update the customer
        public static void UpdateCustomer(Customer updatedCustomer, DateTime updateTime)
        {
            string utcTime = FormatUTCDateTime(updateTime);

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            MySqlTransaction transaction = conn.BeginTransaction();
            var query = $"UPDATE customer" +
                $" SET customerName = '{updatedCustomer.CustomerName}', active = '{Convert.ToInt32(updatedCustomer.IsActive)}', lastUpdateBy = '{CurrentUserName}', lastUpdate = CURRENT_TIMESTAMP" +
                $" WHERE customerId = '{updatedCustomer.CustomerID}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            transaction = conn.BeginTransaction();
            var query2 = $"UPDATE address" +
               $" SET address = '{updatedCustomer.CustomerAddress}', postalCode = '{updatedCustomer.CustomerPostalCode}', phone = '{updatedCustomer.CustomerPhone}', lastUpdateBy = '{CurrentUserName}', lastUpdate = CURRENT_TIMESTAMP" +
               $" WHERE address = '{updatedCustomer.CustomerAddress}'";
            cmd.CommandText = query2;
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            transaction = conn.BeginTransaction();
            var query3 = $"UPDATE city" +
               $" SET city = '{updatedCustomer.CustomerCity}', lastUpdateBy = '{CurrentUserName}', lastUpdate = CURRENT_TIMESTAMP" +
               $" WHERE city = '{updatedCustomer.CustomerCity}'";
            cmd.CommandText = query3;
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            transaction = conn.BeginTransaction();
            var query4 = $"UPDATE country" +
               $" SET country = '{updatedCustomer.CustomerCountry}', lastUpdateBy = '{CurrentUserName}', lastUpdate = CURRENT_TIMESTAMP" +
               $" WHERE country = '{updatedCustomer.CustomerCountry}'";
            cmd.CommandText = query4;
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            conn.Close();
        }

        public static void DeleteCustomer(int custId)
        {
            //int customerId = custId;
            int addressId = 0;
            int cityId = 0;
            int countryId = 0;

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            string getInfo = $"SELECT customer.customerId, address.addressId, city.cityId, country.countryId FROM customer " +
                $"JOIN address ON customer.addressId = address.addressId JOIN city ON address.cityId = city.cityId JOIN country " +
                $"ON city.countryId = country.countryId WHERE customer.customerId = '{custId}'";
            MySqlCommand cmd1 = new MySqlCommand(getInfo, conn);
            MySqlDataReader rdr = cmd1.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Read();
                {
                    //customerId = (int)rdr["customerId"];
                    addressId = rdr.GetInt32(1);
                    cityId = rdr.GetInt32(2);
                    countryId = rdr.GetInt32(3);
                }
            }
            rdr.Close();

            string test = $"DELETE FROM appointment WHERE customerId = '{custId}'";
            string query1 = $"DELETE FROM customer WHERE customerId = '{custId}'";
            string query2 = $"DELETE FROM address WHERE addressId = '{addressId}'";
            string query3 = $"DELETE FROM city WHERE cityId = '{cityId}'";
            string query4 = $"DELETE FROM country WHERE countryId = '{countryId}'";

            MySqlCommand cmd = new MySqlCommand(test, conn);
            MySqlTransaction transaction = conn.BeginTransaction();
            cmd.CommandText = test;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            transaction = conn.BeginTransaction();
            cmd.CommandText = query1;
            cmd.Connection = conn;
            //cmd.Transaction = transaction;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();


            transaction = conn.BeginTransaction();
            cmd.CommandText = query2;
            cmd.Connection = conn;
            //cmd.Transaction = transaction;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            transaction = conn.BeginTransaction();         
            cmd.CommandText = query3;
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            transaction = conn.BeginTransaction();           
            cmd.CommandText = query4;
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();

        }
        public static int CreateCountry(string country)
        {
            int countryId = GetID("country", "countryId") + 1;
            string username = CurrentUserName;
            //DateTime UTCTime = GetDateTime();
            string utcTime = FormatUTCDateTime(Now);

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
            string utcTime = FormatUTCDateTime(Now);

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

        public static int CreateAddress(int cityId, string address, string postalCode, string phone)
        {
            int addressId = GetID("address", "addressId") + 1;
            string username = CurrentUserName;
            //DateTime UTCTime = GetDateTime();
            string utcTime = FormatUTCDateTime(Now);

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            MySqlTransaction transaction = conn.BeginTransaction();

            string query = $"INSERT into address (addressId, address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdateBy)" +
                $"VALUES ('{addressId}', '{address}', '{null}', '{cityId}', '{postalCode}', '{phone}', '{utcTime}', '{username}', '{username}')";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();

            return addressId;

        }
    }
}
