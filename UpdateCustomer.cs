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
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public int CustomerPostalCode { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerCountry { get; set; }
        public bool IsActive { get; set; }

        public UpdateCustomer(int customerId)
        {
            InitializeComponent();
            CustomerId = customerId;
        }

        private void UpdateCustomer_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();
            string query = $"SELECT customer.customerId, customer.customerName, customer.active, address.address, address.phone, address.postalCode, city.city, country.country FROM customer " +
                $"JOIN address ON customer.addressId = address.addressId JOIN city ON address.cityId = city.cityId JOIN country " +
                $"ON city.countryId = country.countryId WHERE customer.customerId = '{CustomerId}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                {
                    CustomerName = rdr.GetString(1);
                    CustomerAddress = rdr.GetString(3);
                    CustomerPhone = rdr.GetString(4);
                    CustomerPostalCode = rdr.GetInt32(5);
                    CustomerCity = rdr.GetString(6);
                    CustomerCountry = rdr.GetString(7);
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
            Customer updatedCustomer = new Customer(CustomerId, CustomerName, CustomerAddress, CustomerPhone, CustomerPostalCode, CustomerCity, CustomerCountry, IsActive);
            DateTime updateTime = Logic.GetDateTime();
            Logic.UpdateCustomer(updatedCustomer, updateTime);
        }

        private void CancelCustomerButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
