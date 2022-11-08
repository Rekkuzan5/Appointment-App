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
            //MySqlCommand cmd = new MySqlCommand(query, conn);
            //MySqlDataReader rd = cmd.ExecuteReader();

            //    while (rd.Read())
            //    {t
            //    customerComboBox.Items.Add(rd[1]);
            //    }


            DataSet ds = new DataSet();
            adapt.Fill(ds, "City");
            cityComboBox.DisplayMember = "city";
            cityComboBox.ValueMember = "cityId";
            cityComboBox.DataSource = ds.Tables["City"];

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
            countryComboBox.DisplayMember = "country";
            countryComboBox.ValueMember = "countryId";
            countryComboBox.DataSource = ds2.Tables["Country"];
            conn.Close();
        }


        private void UpdateCustomerButton_Click(object sender, EventArgs e)
        {
            DialogResult youSure = MessageBox.Show("Are you sure you want to update this customer?", "", MessageBoxButtons.YesNo);
            if (youSure == DialogResult.Yes)
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
                    MessageBox.Show("Customer Record Updated");
                    this.Close();
                }
            }
        }

        private void CancelCustomerButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
