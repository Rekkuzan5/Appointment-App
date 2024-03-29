﻿using System;
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
        private static string path = "logins.txt";
        public static int CurrentUserID { get; set; }
        public static string CurrentUserName { get; set; }
        public static DateTime Now { get; set; }
        public static DateTime UtcNow { get; }
        public static string Path { get => path;}

        public static int VerifyUser(string user, string password)
        {
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
            rdr.Close();
            conn.Close();
            return CurrentUserID;
        }
        conn.Close();
        return 0;
        }

        // this works but could be made into an object for easier coding....this is too convoluted and harder than it should be this way currently
        public static void CheckLoginAppointment(DateTime login)
        {
            List<KeyValuePair<string, DateTime>> appointmentList = new List<KeyValuePair<string, DateTime>>();

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            string query = $"SELECT start FROM appointment WHERE userId = '{CurrentUserID}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                rdr.Read();
                appointmentList.Add(new KeyValuePair<string, DateTime>("start", (DateTime)rdr[0]));
            }
            rdr.Close();
            conn.Close();

            foreach (KeyValuePair<string, DateTime> app in appointmentList)
            {
                TimeSpan diff = app.Value.Subtract(login);
                if (diff.TotalMinutes > 0 && diff.TotalMinutes <= 15)
                {
                    MessageBox.Show("You have an appointment within 15 minutes!", "Alert");
                }
            }
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

        // Gets the id from any table.
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

                //Get Address info
                // *** Lambda for consolidating expression *** //
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

                //Get city info
                // *** Lambda for consolidating expression *** //
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

                //Get country info
                // *** Lambda for consolidating expression *** //
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
        }

        // Create a new country for the database
        public static int CreateCountry(string country)
        {
            string username = CurrentUserName;
            string utcTime = FormatUTCDateTime(Now);
            var countryList = new List<KeyValuePair<string, int>>();

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            string countryNameQuery = $"SELECT countryId, country FROM country WHERE country = '{country}'";
            MySqlCommand cmd1 = new MySqlCommand(countryNameQuery, conn);
            MySqlDataReader rdr = cmd1.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                countryList.Add(new KeyValuePair<string, int>("countryId", (int)rdr[0]));
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
                MessageBox.Show("New country has been added", "Success");
                return newCountryId;
            }

            // *** Lambda for consolidating expression *** //
            var containedCountry = countryList.First(kvp => kvp.Key == "countryId").Value;
            if (containedCountry != 0)
            {
                //MessageBox.Show("This country is present"); -- Test message
                int oldCountryId = (int)countryList.First(kvp => kvp.Key == "countryId").Value;
                return oldCountryId;
            }
            else
            {
                MessageBox.Show("Country cannot be added.", "Error");
                return 0;
            }
        }

        // *** Create a new city for the database
        public static int CreateCity(int countryId, string city)
        {
            int cityId = GetID("city", "cityID") + 1;
            string username = CurrentUserName;
            string utcTime = FormatUTCDateTime(Now);

            var cityList = new List<KeyValuePair<string, int>>();

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            string cityQuery = $"SELECT cityId, city FROM city WHERE city = '{city}'";
            MySqlCommand cmd = new MySqlCommand(cityQuery, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                cityList.Add(new KeyValuePair<string, int>("cityId", (int)rdr[0]));
                rdr.Close();
            }
            else
            {
                rdr.Close();
                int newCityId = GetID("city", "cityId") + 1;

                MySqlTransaction transaction = conn.BeginTransaction();
                string query = $"INSERT into city (cityId, city, countryId, createDate, createdBy, lastUpdateBy)" +
                    $"VALUES ('{newCityId}', '{city}', '{countryId}', CURRENT_TIMESTAMP, '{username}', '{username}')";

                MySqlCommand cmd2 = new MySqlCommand(query, conn);
                cmd2.Transaction = transaction;
                cmd2.ExecuteNonQuery();
                transaction.Commit();
                conn.Close();
                MessageBox.Show("New city has been added.", "Success");
                return newCityId;
            }

            // *** Lambda for consolidating expression *** //
            var containedCity = cityList.First(kvp => kvp.Key == "cityId").Value;
            if (containedCity != 0)
            {
                //MessageBox.Show("This city is present"); -- test message
                int existingCityId = (int)cityList.First(kvp => kvp.Key == "cityId").Value;
                return existingCityId;
            }
            else
            {
                MessageBox.Show("City cannot be added.", "Error");
                return 0;
            }
        }

        public static int CreateAddress(int cityId, string address, string postalCode, string phone)
        {
            int addressId = GetID("address", "addressId") + 1;
            string username = CurrentUserName;
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

        public static DateTime getDateTime2()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.Local);
        }

        // *** Lambda to streamline the date format function *** //
        public static string dateSQLFormat(DateTime dateValue) => dateValue.ToString("yyyy-MM-dd HH:mm:ss");


        public static void createAppointment(int custID, string title, string type, DateTime start, DateTime endTime)
        {
            int appointID = GetID("appointment", "appointmentId") + 1;
            //int userID = 1;

            int currentUserId = CurrentUserID;
            string currentUserName = CurrentUserName;

            start = TimeZoneInfo.ConvertTimeToUtc(start);
            endTime = TimeZoneInfo.ConvertTimeToUtc(endTime);
            DateTime utc = GetDateTime();

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();
            MySqlTransaction transaction = conn.BeginTransaction();
            // Start a local transaction.
            var query = "INSERT into appointment (appointmentId, customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                        $"VALUES ('{appointID}', '{custID}', '{currentUserId}', '{title}', '{null}', '{null}', '{null}', '{type}', '{null}', '{dateSQLFormat(start)}','{dateSQLFormat(endTime)}','{dateSQLFormat(utc)}', '{currentUserName}', CURRENT_TIMESTAMP, '{currentUserName}')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();

        }

        public static bool CompareAppointmentTimes(DateTime appStart, DateTime appEnd)
        {
            DateTime openTime = appStart.Date.AddHours(8).AddMinutes(00).AddSeconds(00);
            DateTime closeTime = appStart.Date.AddHours(17).AddMinutes(00).AddSeconds(00);
            if (appStart >= openTime && appEnd < closeTime)
            {
                // make connection and compare appointment times
                MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
                conn.Open();

                string query = $"SELECT start, end FROM appointment";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                // could probably make this a switch statement
                while (rdr.Read())
                {
                    if (appStart > (DateTime)rdr[0] && appEnd < (DateTime)rdr[1])
                    {
                        MessageBox.Show("Cannot schedule appointment within another appointment!", "Error");
                        return false;
                    }
                    if (appStart > (DateTime)rdr[0] && appStart < (DateTime)rdr[1] && appEnd > (DateTime)rdr[1])
                    {
                        MessageBox.Show("Cannot schedule appointment that overlaps an existing appointment!", "Error");
                        return false;
                    }
                    if (appStart < (DateTime)rdr[0] && appEnd > (DateTime)rdr[1])
                    {
                        MessageBox.Show("Cannot schedule appointment that engulfs existing appointment!", "Error");
                        return false;
                    }
                    if (appStart < (DateTime)rdr[0] && appEnd > (DateTime)rdr[0])
                    {
                        MessageBox.Show("Cannot schedule appointment that would merge into another!", "Error");
                        return false;
                    }
                }
                rdr.Close();
                conn.Close();
                return true;
            }
            else
            {
                MessageBox.Show("This appointment is outside of business hours.", "Error");
                return false;
            }
            
        }

        public static void UpdateAppointment(int appId, string type, string title, DateTime start, DateTime endTime)
        {
            int currentUserId = CurrentUserID;
            string currentUserName = CurrentUserName;

            start = TimeZoneInfo.ConvertTimeToUtc(start);
            endTime = TimeZoneInfo.ConvertTimeToUtc(endTime);

            DateTime utc = GetDateTime();

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();
            MySqlTransaction transaction = conn.BeginTransaction(); ;

            var query = $"UPDATE appointment SET userId = '{ currentUserId }', title = '{ title }', description = '{null}', location = '{null}', contact = '{null}', type = '{ type }', url = '{null}', start = '{dateSQLFormat(start)}', end = '{dateSQLFormat(endTime)}', createDate = '{dateSQLFormat(utc)}', createdBy = '{currentUserName}', lastUpdate = CURRENT_TIMESTAMP, lastUpdateBy = '{currentUserName}' WHERE appointmentId = '{ appId }'";
            
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
