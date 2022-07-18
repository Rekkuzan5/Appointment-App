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
                    name = rdr.GetString(0);

                }
            }
            rdr.Close();
            label8.Text = $"name is {name}";
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
