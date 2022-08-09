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
        public static string CustomerName { get; set; }
        public static string CustomerAddressId { get; set; }
        public static string CustomerAddress { get; set; }
        public static string CustomerPhone { get; set; }
        public static int CustomerPostalCode { get; set; }
        public static string CustomerCity { get; set; }
        public static string CustomerCountry { get; set; }
        public static bool IsActive { get; set; }

        public UpdateCustomer(int customerId)
        {
            InitializeComponent();
            CustomerId = customerId;
        }

        private void UpdateCustomer_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();
            string query = $"SELECT customer.customerId, customer.customerName, customer.active, address.addressId, address.address, address.phone, address.postalCode, city.city, country.country FROM customer " +
                $"JOIN address ON customer.addressId = address.addressId JOIN city ON address.cityId = city.cityId JOIN country " +
                $"ON city.countryId = country.countryId WHERE customer.customerId = '{CustomerId}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                {
                    CustomerName = rdr.GetString(1);
                    CustomerAddressId = rdr.GetString(3);
                    CustomerAddress = rdr.GetString(4);
                    CustomerPhone = rdr.GetString(5);
                    CustomerPostalCode = rdr.GetInt32(6);
                    CustomerCity = rdr.GetString(7);
                    CustomerCountry = rdr.GetString(8);
                    IsActive = rdr.GetBoolean(2);
                }
            }
            rdr.Close();
            ActiveCustomerCheck.Checked = IsActive;
            customerNameTextBox.Text = CustomerName;
            customerAddressTextBox.Text = CustomerAddress;
            customerPhoneTextBox.Text = CustomerPhone;
            customerZipTextBox.Text = CustomerPostalCode.ToString();
            customerCityTextBox.Text = CustomerCity;
            customerCountryTextbox.Text = CustomerCountry;
        }

        private void UpdateCustomerButton_Click(object sender, EventArgs e)
        {
            IsActive = ActiveCustomerCheck.Checked;
            CustomerName = customerNameTextBox.Text;
            CustomerAddress = customerAddressTextBox.Text;
            CustomerPhone = customerPhoneTextBox.Text;
            CustomerPostalCode = Convert.ToInt32(customerZipTextBox.Text);
            CustomerCity = customerCityTextBox.Text;
            CustomerCountry = customerCountryTextbox.Text;

            Customer updatedCustomer = new Customer(CustomerId, CustomerName, CustomerAddressId, CustomerAddress, CustomerPhone, CustomerPostalCode, CustomerCity, CustomerCountry, IsActive);
            DateTime updateTime = Logic.GetDateTime();
            Logic.UpdateCustomer(updatedCustomer, updateTime);
            this.Close();
        }

        private void CancelCustomerButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
