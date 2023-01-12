using Appointment_App.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            FillCityData();
        }

        public static List<KeyValuePair<string, object>> CustList;

        private void UpdateCustomer_Load(object sender, EventArgs e)
        {
            CustList = Logic.SearchCustomer(CustomerId);
            FillFields(CustList);
        }

        private void FillFields(List<KeyValuePair<string, object>> custList)
        {
            // Lambda used to set text values from kvp
            customerNameTextBox.Text = custList.First(kvp => kvp.Key == "customerName").Value.ToString();
            customerPhoneTextBox.Text = custList.First(kvp => kvp.Key == "phone").Value.ToString();
            customerAddressTextBox.Text = custList.First(kvp => kvp.Key == "address").Value.ToString();
            cityComboBox.Text = custList.First(kvp => kvp.Key == "city").Value.ToString();
            customerZipTextBox.Text = custList.First(kvp => kvp.Key == "postalCode").Value.ToString();
            countryComboBox.Text = custList.First(kvp => kvp.Key == "country").Value.ToString();
            if (Convert.ToInt32(custList.First(kvp => kvp.Key == "active").Value) == 1)
            {
                ActiveCustomerCheck.Checked = true;
            }
            else
            {
                ActiveCustomerCheck.Checked = false;
            }
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
            cityComboBox.DisplayMember = "city";
            cityComboBox.ValueMember = "cityId";
            cityComboBox.DataSource = ds.Tables["City"];

            // Look for customers
            string query2 = $"SELECT countryId, country FROM country";
            MySqlDataAdapter adapt2 = new MySqlDataAdapter(query2, conn);

            DataSet ds2 = new DataSet();
            adapt2.Fill(ds2, "Country");
            countryComboBox.DisplayMember = "country";
            countryComboBox.ValueMember = "countryId";
            countryComboBox.DataSource = ds2.Tables["Country"];
            conn.Close();
        }

        // validation for fields
        private bool ValidateInformation()
        {
            bool result = false;
            if (string.IsNullOrEmpty(customerNameTextBox.Text) ||
                string.IsNullOrEmpty(customerAddressTextBox.Text) ||
                string.IsNullOrEmpty(customerPhoneTextBox.Text) ||
                string.IsNullOrEmpty(customerZipTextBox.Text) ||
                string.IsNullOrEmpty(cityComboBox.Text) ||
                string.IsNullOrEmpty(countryComboBox.Text))
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


        private void UpdateCustomerButton_Click(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show("Are you sure you want to update this customer?", "Proceed?", MessageBoxButtons.YesNo);
            if (youSure == DialogResult.Yes)
            {
                if (ValidateInformation())
                {

                    try
                    {
                        //Grab List & convert
                        var list = CustList;
                        IDictionary<string, object> dictionary = list.ToDictionary(pair => pair.Key, pair => pair.Value);
                        //replace values for the keys in the form         
                        dictionary["customerName"] = customerNameTextBox.Text;
                        dictionary["phone"] = customerPhoneTextBox.Text;
                        dictionary["address"] = customerAddressTextBox.Text;
                        dictionary["city"] = cityComboBox.Text;
                        dictionary["postalCode"] = customerZipTextBox.Text;
                        dictionary["country"] = countryComboBox.Text;
                        dictionary["active"] = ActiveCustomerCheck.Checked ? 1 : 0;

                        //Pass the updated IDictionary to dbhelper to update the database
                        Logic.UpdateCustomer(dictionary);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        MessageBox.Show("Customer Record Updated", "Success");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("validation failed", "Error");
                }
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

        private void CancelCustomerButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
