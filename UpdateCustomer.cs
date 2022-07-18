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

        public UpdateCustomer(int customerId)
        {
            InitializeComponent();
            CustomerId = customerId;
        }

        private void UpdateCustomer_Load(object sender, EventArgs e)
        {
            string name = null;
            string address = null;
            string phone = null;
            int postalCode = 0;
            string city = null;
            string country = null;

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            conn.Open();
            string query = $"SELECT customer.customerId, customer.customerName, address.address, address.phone, address.postalCode, city.city, country.country FROM customer " +
                $"JOIN address ON customer.addressId = address.addressId JOIN city ON address.cityId = city.cityId JOIN country " +
                $"ON city.countryId = country.countryId WHERE customer.customerId = '{CustomerId}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                {
                    name = rdr.GetString(1);
                    address = rdr.GetString(2);
                    phone = rdr.GetString(3);
                    postalCode = rdr.GetInt32(4);
                    city = rdr.GetString(5);
                    country = rdr.GetString(6);
                }
            }
            rdr.Close();
            customerNameTextBox.Text = name;
            customerAddressTextBox.Text = address;
            customerPhoneTextBox.Text = phone;
            customerZipTextBox.Text = postalCode.ToString();
            customerCityTextBox.Text = city;
            customerCountryTextbox.Text = country;
        }

        private void UpdateCustomerButton_Click(object sender, EventArgs e)
        {

        }

        private void CancelCustomerButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
