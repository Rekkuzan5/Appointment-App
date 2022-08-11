using Appointment_App.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appointment_App
{
    public partial class UpdateCustomer : Form
    {
        public static int CustomerId { get; set; }
        //public static string CustomerName { get; set; }
        //public static string CustomerAddressId { get; set; }
        //public static string CustomerAddress { get; set; }
        //public static string CustomerPhone { get; set; }
        //public static int CustomerPostalCode { get; set; }
        //public static string CustomerCity { get; set; }
        //public static string CustomerCountry { get; set; }
        //public static bool IsActive { get; set; }

        public UpdateCustomer(int customerId)
        {
            InitializeComponent();
            CustomerId = customerId;
        }

        public static List<KeyValuePair<string, object>> CustList;


        private void UpdateCustomer_Load(object sender, EventArgs e)
        {
            //MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            //conn.Open();
            //string query = $"SELECT customer.customerId, customer.customerName, customer.active, address.addressId, address.address, address.phone, address.postalCode, city.city, country.country FROM customer " +
            //    $"JOIN address ON customer.addressId = address.addressId JOIN city ON address.cityId = city.cityId JOIN country " +
            //    $"ON city.countryId = country.countryId WHERE customer.customerId = '{CustomerId}'";
            //MySqlCommand cmd = new MySqlCommand(query, conn);
            //MySqlDataReader rdr = cmd.ExecuteReader();

            //if (rdr.HasRows)
            //{
            //    rdr.Read();
            //    {
            //        CustomerName = rdr.GetString(1);
            //        CustomerAddressId = rdr.GetString(3);
            //        CustomerAddress = rdr.GetString(4);
            //        CustomerPhone = rdr.GetString(5);
            //        CustomerPostalCode = rdr.GetInt32(6);
            //        CustomerCity = rdr.GetString(7);
            //        CustomerCountry = rdr.GetString(8);
            //        IsActive = rdr.GetBoolean(2);
            //    }
            //}
            //rdr.Close();


            //ActiveCustomerCheck.Checked = IsActive;
            //customerNameTextBox.Text = CustomerName;
            //customerAddressTextBox.Text = CustomerAddress;
            //customerPhoneTextBox.Text = CustomerPhone;
            //customerZipTextBox.Text = CustomerPostalCode.ToString();
            //customerCityTextBox.Text = CustomerCity;
            //customerCountryTextbox.Text = CustomerCountry;

            CustList = Logic.SearchCustomer(CustomerId);

        }

        private void fillFields(List<KeyValuePair<string, object>> custList)
        {
            // Lambda used to set text values from kvp
            customerNameTextBox.Text = custList.First(kvp => kvp.Key == "customerName").Value.ToString();
            customerPhoneTextBox.Text = custList.First(kvp => kvp.Key == "phone").Value.ToString();
            customerAddressTextBox.Text = custList.First(kvp => kvp.Key == "address").Value.ToString();
            customerCityTextBox.Text = custList.First(kvp => kvp.Key == "city").Value.ToString();
            customerZipTextBox.Text = custList.First(kvp => kvp.Key == "postalCode").Value.ToString();
            customerCountryTextbox.Text = custList.First(kvp => kvp.Key == "country").Value.ToString();
            if (Convert.ToInt32(custList.First(kvp => kvp.Key == "active").Value) == 1)
            {
                ActiveCustomerCheck.Checked = true;
            }
            else
            {
                ActiveCustomerCheck.Checked = false;
            }
        }
        //private void UpdateCustomerButton_Click(object sender, EventArgs e)
        //{
        //    IsActive = ActiveCustomerCheck.Checked;
        //    CustomerName = customerNameTextBox.Text;
        //    CustomerAddress = customerAddressTextBox.Text;
        //    CustomerPhone = customerPhoneTextBox.Text;
        //    CustomerPostalCode = Convert.ToInt32(customerZipTextBox.Text);
        //    CustomerCity = customerCityTextBox.Text;
        //    CustomerCountry = customerCountryTextbox.Text;

        //    Customer updatedCustomer = new Customer(CustomerId, CustomerName, CustomerAddressId, CustomerAddress, CustomerPhone, CustomerPostalCode, CustomerCity, CustomerCountry, IsActive);
        //    DateTime updateTime = Logic.GetDateTime();
        //    Logic.UpdateCustomer(updatedCustomer, updateTime);
        //    this.Close();
        //}

        private void CancelCustomerButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Update the customer //!!!!!!! Need to do the same as updating address in the function below...maybe try to clean it up or more streamlined somehow??? !!!!!!! //
        //public static void UpdateCustomerData(Customer updatedCustomer, DateTime updateTime)
        //{
        //    //string utcTime = FormatUTCDateTime(updateTime);

        //    MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
        //    conn.Open();

        //    MySqlTransaction transaction = conn.BeginTransaction();
        //    var query = $"UPDATE customer" +
        //        $" SET customerName = '{updatedCustomer.CustomerName}', active = '{Convert.ToInt32(updatedCustomer.IsActive)}', lastUpdateBy = '{CurrentUserName}', lastUpdate = CURRENT_TIMESTAMP" +
        //        $" WHERE customerId = '{updatedCustomer.CustomerID}'";
        //    MySqlCommand cmd = new MySqlCommand(query, conn);
        //    cmd.Transaction = transaction;
        //    cmd.ExecuteNonQuery();
        //    transaction.Commit();

        //    transaction = conn.BeginTransaction();
        //    var query2 = $"UPDATE address" +
        //       $" SET address = '{updatedCustomer.CustomerAddress}', postalCode = '{updatedCustomer.CustomerPostalCode}', phone = '{updatedCustomer.CustomerPhone}', lastUpdateBy = '{CurrentUserName}', lastUpdate = CURRENT_TIMESTAMP" +
        //       $" WHERE addressId = '{updatedCustomer.CustomerAddressId}'";
        //    cmd.CommandText = query2;
        //    cmd.Connection = conn;
        //    cmd.Transaction = transaction;
        //    cmd.ExecuteNonQuery();
        //    transaction.Commit();

        //    transaction = conn.BeginTransaction();
        //    var query3 = $"UPDATE city" +
        //       $" SET city = '{updatedCustomer.CustomerCity}', lastUpdateBy = '{CurrentUserName}', lastUpdate = CURRENT_TIMESTAMP" +
        //       $" WHERE city = '{updatedCustomer.CustomerCity}'";
        //    cmd.CommandText = query3;
        //    cmd.Connection = conn;
        //    cmd.Transaction = transaction;
        //    cmd.ExecuteNonQuery();
        //    transaction.Commit();

        //    transaction = conn.BeginTransaction();
        //    var query4 = $"UPDATE country" +
        //       $" SET country = '{updatedCustomer.CustomerCountry}', lastUpdateBy = '{CurrentUserName}', lastUpdate = CURRENT_TIMESTAMP" +
        //       $" WHERE country = '{updatedCustomer.CustomerCountry}'";
        //    cmd.CommandText = query4;
        //    cmd.Connection = conn;
        //    cmd.Transaction = transaction;
        //    cmd.ExecuteNonQuery();
        //    transaction.Commit();

        //    conn.Close();
        //}
        //public bool updateCustomer(Dictionary<string, string> updatedForm)
        //{
        //    MySqlConnection c = new MySqlConnection(DBConnection.Connection);
        //    c.Open();

        //    // Updates Customer Table
        //    string recUpdate = $"UPDATE customer" +
        //        $" SET customerName = '{updatedForm["customerName"]}', active = '{updatedForm["active"]}', lastUpdate = '{DataHelper.createTimestamp()}', lastUpdateBy = '{Logic.CurrentUserName}'" +
        //        $" WHERE customerName = '{cForm["customerName"]}'";
        //    MySqlCommand cmd = new MySqlCommand(recUpdate, c);
        //    int customerUpdated = cmd.ExecuteNonQuery();

        //    // Updates Address Table
        //    recUpdate = $"UPDATE address" +
        //        $" SET address = '{updatedForm["address"]}', postalCode = '{updatedForm["zip"]}', phone = '{updatedForm["phone"]}', lastUpdate = '{DataHelper.createTimestamp()}', lastUpdateBy = '{DataHelper.getCurrentUserName()}'" +
        //        $" WHERE address = '{cForm["address"]}'";
        //    cmd = new MySqlCommand(recUpdate, c);
        //    int addressUpdated = cmd.ExecuteNonQuery();

        //    // Updates City Table
        //    recUpdate = $"UPDATE city" +
        //        $" SET city = '{updatedForm["city"]}', lastUpdate = '{DataHelper.createTimestamp()}', lastUpdateBy = '{DataHelper.getCurrentUserName()}'" +
        //        $" WHERE city = '{cForm["city"]}'";
        //    cmd = new MySqlCommand(recUpdate, c);
        //    int cityUpdated = cmd.ExecuteNonQuery();

        //    // Updates Country Table
        //    recUpdate = $"UPDATE country" +
        //        $" SET country = '{updatedForm["country"]}', lastUpdate = '{DataHelper.createTimestamp()}', lastUpdateBy = '{DataHelper.getCurrentUserName()}'" +
        //        $" WHERE country = '{cForm["country"]}'";
        //    cmd = new MySqlCommand(recUpdate, c);
        //    int countryUpdated = cmd.ExecuteNonQuery();

        //    c.Close();

        //    if (customerUpdated != 0 && addressUpdated != 0 && cityUpdated != 0 && countryUpdated != 0)
        //        return true;
        //    else
        //        return false;
        //}

    }
}
