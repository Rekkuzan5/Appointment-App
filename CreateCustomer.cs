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
    public partial class CreateCustomer : Form
    {
        public CreateCustomer()
        {
            InitializeComponent();
            FillCityData();
        }

        private void CreateCustomer_Load(object sender, EventArgs e)
        {

        }

        private void CancelCustomerButton_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        // *** need to polish this section but it is working partially *** //
        public void FillCityData()
        {
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            conn.Open();
            // Look for customers
            string query = $"SELECT cityId, city FROM city";
            MySqlDataAdapter adapt = new MySqlDataAdapter(query, conn);
            //MySqlCommand cmd = new MySqlCommand(query, conn);
            //MySqlDataReader rd = cmd.ExecuteReader();

            //    while (rd.Read())
            //    {t
            //    customerComboBox.Items.Add(rd[1]);
            //    }


            DataSet ds = new DataSet();
            adapt.Fill(ds, "City");
            cityComboTextBox.DisplayMember = "city";
            cityComboTextBox.ValueMember = "cityId";
            cityComboTextBox.DataSource = ds.Tables["City"];

            // Look for customers
            string query2 = $"SELECT countryId, country FROM country";
            MySqlDataAdapter adapt2 = new MySqlDataAdapter(query2, conn);
            //MySqlCommand cmd = new MySqlCommand(query, conn);
            //MySqlDataReader rd = cmd.ExecuteReader();

            //    while (rd.Read())
            //    {t
            //    customerComboBox.Items.Add(rd[1]);
            //    }


            DataSet ds2 = new DataSet();
            adapt2.Fill(ds2, "Country");
            customerCountryCombobox.DisplayMember = "country";
            customerCountryCombobox.ValueMember = "countryId";
            customerCountryCombobox.DataSource = ds2.Tables["Country"];
            conn.Close();
        }

        private void CreateCustomerButton_Click(object sender, EventArgs e)
        {
            //DateTime timestamp = Logic.GetDateTime();
            //string username = Logic.CurrentUserName;

            if (string.IsNullOrEmpty(customerNameTextBox.Text) ||
                string.IsNullOrEmpty(customerAddressTextBox.Text) ||
                string.IsNullOrEmpty(customerPhoneTextBox.Text) ||
                string.IsNullOrEmpty(customerZipTextBox.Text) ||
                string.IsNullOrEmpty(cityComboTextBox.Text) ||
                string.IsNullOrEmpty(customerCountryCombobox.Text)
                )
            {
                MessageBox.Show("Please enter information in all fields.");
            }
            else
            {
                int active = 1;
                //int customerID = Logic.GetID("customer", "customerId") + 1;
                string username = Logic.CurrentUserName;
                int countryID = Logic.CreateCountry(customerCountryCombobox.Text);
                int cityID = Logic.CreateCity(countryID, cityComboTextBox.Text);
                int addressID = Logic.CreateAddress(cityID, customerAddressTextBox.Text, customerZipTextBox.Text, customerPhoneTextBox.Text);

                Logic.CreateCustomer(customerNameTextBox.Text, addressID, active, Logic.Now, username);

                this.Close();

            }
            //Create country table record

            //Create city table record

            //Create address table record

            //Create customer record
        }
    }
}
