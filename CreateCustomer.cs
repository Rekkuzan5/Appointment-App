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
using System.Text.RegularExpressions;

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

        public void FillCityData()
        {
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            conn.Open();
            // Look for customers
            string query = $"SELECT cityId, city FROM city";
            MySqlDataAdapter adapt = new MySqlDataAdapter(query, conn);

            DataSet ds = new DataSet();
            adapt.Fill(ds, "City");
            cityComboTextBox.DisplayMember = "city";
            cityComboTextBox.ValueMember = "cityId";
            cityComboTextBox.DataSource = ds.Tables["City"];

            // Look for customers
            string query2 = $"SELECT countryId, country FROM country";
            MySqlDataAdapter adapt2 = new MySqlDataAdapter(query2, conn);

            DataSet ds2 = new DataSet();
            adapt2.Fill(ds2, "Country");
            customerCountryCombobox.DisplayMember = "country";
            customerCountryCombobox.ValueMember = "countryId";
            customerCountryCombobox.DataSource = ds2.Tables["Country"];
            conn.Close();
        }

        // Need to create a validation function that checks for the correct format of the information to pass into the create customer function
        private bool ValidateInformation()
        {
            bool result = false;
            if (string.IsNullOrEmpty(customerNameTextBox.Text) ||
                string.IsNullOrEmpty(customerAddressTextBox.Text) ||
                string.IsNullOrEmpty(customerPhoneTextBox.Text) ||
                string.IsNullOrEmpty(customerZipTextBox.Text) ||
                string.IsNullOrEmpty(cityComboTextBox.Text) ||
                string.IsNullOrEmpty(customerCountryCombobox.Text))
            {
                MessageBox.Show("Please enter information in all fields.", "Error");
                return false;
            }
            bool phoneResult = isValidPhoneNumber(customerPhoneTextBox.Text);
            bool zipResult = isValidZipCode(customerZipTextBox.Text);
            
            if (phoneResult && zipResult)
            {
                result = true;
            }
            return result;
        }

        private void CreateCustomerButton_Click(object sender, EventArgs e)
        {
            if (ValidateInformation())
            {
                MessageBox.Show("Customer successfully created.", "Success");

                int active = 1;
                string username = Logic.CurrentUserName;
                int countryID = Logic.CreateCountry(customerCountryCombobox.Text);
                int cityID = Logic.CreateCity(countryID, cityComboTextBox.Text);
                int addressID = Logic.CreateAddress(cityID, customerAddressTextBox.Text, customerZipTextBox.Text, customerPhoneTextBox.Text);

                Logic.CreateCustomer(customerNameTextBox.Text, addressID, active, Logic.Now, username);

                this.Close();
            }
            else
            {
                MessageBox.Show("validation failed", "Error");
            }
        }

        // validation for customer name by limiting user input to only alpha characters
        private void customerNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }

        public bool isValidPhoneNumber(string phonenumber)
        {
            // Regex string to check against
            string regex = @"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$";

            Regex p = new Regex(regex);

            if (phonenumber == null)
            {
                return false;
            }

            Match m = p.Match(phonenumber);

            return m.Success;
        }

        private bool isValidZipCode(string zip)
        {
            Regex z = new Regex(@"([0-9]{5})");
            if (z.IsMatch(zip))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Prevent user from entering alphacharacters that are not numbers into textbox
        private void customerPhoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void customerZipTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
