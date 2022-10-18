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
            string formatSQLTime = Now.ToString("yyyy-MM-dd hh:mm");
            return formatSQLTime;
        }

        // Gets the id from any table.  New customers will not need this since the table auto increments userID for us.
        // *** this may need to be changed or a new method for just getting the id that isn't the max type ***
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

        // Get customer data for updating -- new code for testing using dictionary data structure
        public static List<KeyValuePair<string, object>> SearchCustomer(int customerID)
        {
            var customerList = new List<KeyValuePair<string, object>>();
            //Get customer Table info
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();
            var query = $"SELECT * FROM customer WHERE customerId = {customerID}";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();
            try
            {
                if (rdr.HasRows)
                {
                    rdr.Read();
                    customerList.Add(new KeyValuePair<string, object>("customerId", rdr[0]));
                    customerList.Add(new KeyValuePair<string, object>("customerName", rdr[1]));
                    customerList.Add(new KeyValuePair<string, object>("addressId", rdr[2]));
                    customerList.Add(new KeyValuePair<string, object>("active", rdr[3]));
                    rdr.Close();
                }
                else
                {
                    MessageBox.Show("No Customer found with the ID: " + customerID, "Please try again");
                    return null;
                }

                //Get Address info now that we have addressID
                var addressID = customerList.First(kvp => kvp.Key == "addressId").Value;

                var query2 = $"SELECT * FROM address WHERE addressId = {addressID}";
                cmd.CommandText = query2;
                cmd.Connection = conn;
                MySqlDataReader rdr2 = cmd.ExecuteReader();
                if (rdr2.HasRows)
                {
                    rdr2.Read();
                    customerList.Add(new KeyValuePair<string, object>("address", rdr2[1]));
                    customerList.Add(new KeyValuePair<string, object>("cityId", rdr2[3]));
                    customerList.Add(new KeyValuePair<string, object>("postalCode", rdr2[4]));
                    customerList.Add(new KeyValuePair<string, object>("phone", rdr2[5]));
                    rdr2.Close();
                }

                //Get city info now that we have cityID
                var cityID = customerList.First(kvp => kvp.Key == "cityId").Value;

                var query3 = $"SELECT * FROM city WHERE cityId = {cityID}";
                cmd.CommandText = query3;
                cmd.Connection = conn;
                MySqlDataReader rdr3 = cmd.ExecuteReader();
                if (rdr3.HasRows)
                {
                    rdr3.Read();
                    customerList.Add(new KeyValuePair<string, object>("city", rdr3[1]));
                    customerList.Add(new KeyValuePair<string, object>("countryId", rdr3[2]));
                    rdr3.Close();
                }

                //Get country info now that we have countryId
                var countryID = customerList.First(kvp => kvp.Key == "countryId").Value;

                var query4 = $"SELECT * FROM country WHERE countryId = {countryID}";
                cmd.CommandText = query4;
                cmd.Connection = conn;
                MySqlDataReader rdr4 = cmd.ExecuteReader();
                if (rdr4.HasRows)
                {
                    rdr4.Read();
                    customerList.Add(new KeyValuePair<string, object>("country", rdr4[1]));
                    rdr4.Close();
                }

                return customerList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        // Create Customer function will go here.
        public static void CreateCustomer(string name, int addressId, int active, DateTime time, string username)
        {
            string utcTime = FormatUTCDateTime(time);

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            MySqlTransaction transaction = conn.BeginTransaction();

            string query = $"INSERT into customer (customerName, addressId, active, createDate, createdBy, lastUpdateBy)" +
                $"VALUES ('{name}', '{addressId}', {active}, CURRENT_TIMESTAMP, '{username}', '{username}')";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();
        }

        public static void UpdateCustomer(IDictionary<string, object> dictionary)
        {
            string user = CurrentUserName;
            //DateTime utc = getDateTime();

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();
            MySqlTransaction transaction = conn.BeginTransaction();
            var query = $"UPDATE country" +
                $" SET country = '{dictionary["country"]}', lastUpdateBy = '{user}'" +
                $" WHERE countryId = '{dictionary["countryId"]}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            // Start a city transaction.
            transaction = conn.BeginTransaction();
            var query2 = $"UPDATE city" +
                $" SET city = '{dictionary["city"]}', lastUpdateBy = '{user}'" +
                $" WHERE cityId = '{dictionary["cityId"]}'";
            cmd.CommandText = query2;
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            // Start a address transaction.
            transaction = conn.BeginTransaction();
            var query3 = $"UPDATE address" +
                $" SET address = '{dictionary["address"]}', lastUpdateBy = '{user}', postalCode = '{dictionary["postalCode"]}', phone = '{dictionary["phone"]}'" +
                $" WHERE addressId = '{dictionary["addressId"]}'";
            cmd.CommandText = query3;
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            // Start a customer transaction.
            transaction = conn.BeginTransaction();
            var query4 = $"UPDATE customer" +
                $" SET customerName = '{dictionary["customerName"]}', lastUpdateBy = '{user}', active = '{dictionary["active"]}'" +
                $" WHERE customerId = '{dictionary["customerId"]}'";
            cmd.CommandText = query4;
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();
        }

        public static void DeleteCustomer(IDictionary<string, object> dictionary)
        {
            //Fix this for proper deleting of customers.
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();
            var query5 = $"DELETE FROM appointment" +
               $" WHERE customerId = '{dictionary["customerId"]}'";
            MySqlCommand cmd = new MySqlCommand(query5, conn);
            MySqlTransaction transaction = conn.BeginTransaction();

            cmd.CommandText = query5;
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            // Start a customer transaction.
            transaction = conn.BeginTransaction();
            var query4 = $"DELETE FROM customer" +
                $" WHERE customerId = '{dictionary["customerId"]}'";
            cmd.CommandText = query4;
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            // Start a address transaction.
            transaction = conn.BeginTransaction();
            var query3 = $"DELETE FROM address" +
                $" WHERE addressId = '{dictionary["addressId"]}'";
            cmd.CommandText = query3;
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            // *** Deleted since the cities and countries don't need to be deleted.  They could be added to but never deleted by a customer/user ***

            //// Start a city transaction.
            //transaction = conn.BeginTransaction();
            //var query2 = $"DELETE FROM city" +
            //    $" WHERE cityId = '{dictionary["cityId"]}'";
            //cmd.CommandText = query2;
            //cmd.Connection = conn;
            //cmd.Transaction = transaction;
            //cmd.ExecuteNonQuery();
            //transaction.Commit();

            //// Start a country transaction.
            //transaction = conn.BeginTransaction();
            //var query = $"DELETE FROM country" +
            //    $" WHERE countryId = '{dictionary["countryId"]}'";
            //cmd.CommandText = query;
            //cmd.Connection = conn;
            //cmd.Transaction = transaction;
            //cmd.ExecuteNonQuery();
            //transaction.Commit();
            //conn.Close();

            // *** End of comment ***

            //int addressId = 0;
            //int cityId = 0;
            //int countryId = 0;

            //MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            //conn.Open();

            //string getInfo = $"SELECT customer.customerId, address.addressId, city.cityId, country.countryId FROM customer " +
            //    $"JOIN address ON customer.addressId = address.addressId JOIN city ON address.cityId = city.cityId JOIN country " +
            //    $"ON city.countryId = country.countryId WHERE customer.customerId = '{custId}'";
            //MySqlCommand cmd1 = new MySqlCommand(getInfo, conn);
            //MySqlDataReader rdr = cmd1.ExecuteReader();
            //if (rdr.HasRows)
            //{
            //    rdr.Read();
            //    {
            //        addressId = rdr.GetInt32(1);
            //        cityId = rdr.GetInt32(2);
            //        countryId = rdr.GetInt32(3);
            //    }
            //}
            //rdr.Close();

            //string test = $"DELETE FROM appointment WHERE customerId = '{custId}'";
            //string query1 = $"DELETE FROM customer WHERE customerId = '{custId}'";
            //string query2 = $"DELETE FROM address WHERE addressId = '{addressId}'";
            //string query3 = $"DELETE FROM city WHERE cityId = '{cityId}'";
            //string query4 = $"DELETE FROM country WHERE countryId = '{countryId}'";

            //MySqlCommand cmd = new MySqlCommand(test, conn);
            //MySqlTransaction transaction = conn.BeginTransaction();
            //cmd.CommandText = test;
            //cmd.Connection = conn;
            //cmd.ExecuteNonQuery();
            //transaction.Commit();

            //transaction = conn.BeginTransaction();
            //cmd.CommandText = query1;
            //cmd.Connection = conn;
            ////cmd.Transaction = transaction;
            //cmd.Transaction = transaction;
            //cmd.ExecuteNonQuery();
            //transaction.Commit();


            //transaction = conn.BeginTransaction();
            //cmd.CommandText = query2;
            //cmd.Connection = conn;
            ////cmd.Transaction = transaction;
            //cmd.Transaction = transaction;
            //cmd.ExecuteNonQuery();
            //transaction.Commit();

            //transaction = conn.BeginTransaction();         
            //cmd.CommandText = query3;
            //cmd.Connection = conn;
            //cmd.Transaction = transaction;
            //cmd.ExecuteNonQuery();
            //transaction.Commit();

            //transaction = conn.BeginTransaction();           
            //cmd.CommandText = query4;
            //cmd.Connection = conn;
            //cmd.Transaction = transaction;
            //cmd.ExecuteNonQuery();
            //transaction.Commit();
            //conn.Close();
        }

        // *** Needs to be a button for creating a new country because a country doesn't need to be input into the system unless new ***
        public static int CreateCountry(string country)
        {
            string username = CurrentUserName;
            string utcTime = FormatUTCDateTime(Now);
            var customerList = new List<KeyValuePair<string, int>>();

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            string countryNameQuery = $"SELECT countryId, country FROM country WHERE country = '{country}'";
            MySqlCommand cmd1 = new MySqlCommand(countryNameQuery, conn);
            MySqlDataReader rdr = cmd1.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                customerList.Add(new KeyValuePair<string, int>("countryId", (int)rdr[0]));
                rdr.Close();
            }
            else
            {
                rdr.Close();
                int newCountryId = GetID("country", "countryId") + 1;

                MySqlTransaction transaction = conn.BeginTransaction();
                string query = $"INSERT into country (countryId, country, createDate, createdBy, lastUpdateBy)" +
                    $"VALUES ('{newCountryId}', '{country}', CURRENT_TIMESTAMP, '{username}', '{username}')";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                conn.Close();
                MessageBox.Show("Country has been added to Database");
                return newCountryId;
            }

            var containedCountry = customerList.First(kvp => kvp.Key == "countryId").Value;
            if (containedCountry != 0)
            {
                MessageBox.Show("This country is present");
                int oldCountryId = (int)customerList.First(kvp => kvp.Key == "countryId").Value;
                return oldCountryId;
            }
            else
            {
                MessageBox.Show("Error: Country cannot be added.");
                return 0;
            }
        }

        // *** Needs to be a button for creating a new city because a city doesn't need to be input into the system unless new ***
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
                $"VALUES ('{cityId}', '{city}', '{countryId}', CURRENT_TIMESTAMP, '{username}', '{username}')";

            MySqlCommand cmd = new MySqlCommand(query, conn)
            {
                Transaction = transaction
            };
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();

            return cityId;
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
                $"VALUES ('{addressId}', '{address}', '{null}', '{cityId}', '{postalCode}', '{phone}', CURRENT_TIMESTAMP, '{username}', '{username}')";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();

            return addressId;

        }

        // Work on these functions. //
        public static DateTime getDateTime()
        {
            return DateTime.Now.ToUniversalTime();

        }

        public static string dateSQLFormat(DateTime dateValue)
        {
            string formatForMySql = dateValue.ToString("yyyy-MM-dd HH:mm");

            return formatForMySql;
        }

        // Need to fix this where the date is actually in the start combo/calendar box //
        public static void createAppointment(int custID, string title, string description, string location, string contact, string type, DateTime start, DateTime endTime)
        {
            int appointID = GetID("appointment", "appointmentId") + 1;
            //int userID = 1;

            int currentUserId = CurrentUserID;
            string currentUserName = CurrentUserName;

            DateTime utc = getDateTime();

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();
            MySqlTransaction transaction = conn.BeginTransaction(); ;
            // Start a local transaction.
            var query = "INSERT into appointment (appointmentId, customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                        $"VALUES ('{appointID}', '{custID}', '{currentUserId}', '{title}', '{description}', '{location}', '{contact}', '{type}', '{null}', '{dateSQLFormat(start)}','{dateSQLFormat(endTime)}','{dateSQLFormat(utc)}', '{currentUserName}', CURRENT_TIMESTAMP, '{currentUserName}')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();

        }

        public static void DeleteAppointment(int appointmentID)
        {
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();
            var query = $"DELETE FROM appointment WHERE appointment.appointmentId = '{appointmentID}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlTransaction transaction = conn.BeginTransaction();
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();
        }
    }
}
