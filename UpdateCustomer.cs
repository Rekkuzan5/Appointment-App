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
            fillFields(CustList);

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

        private void CancelCustomerButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
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
                    dictionary["city"] = customerCityTextBox.Text;
                    dictionary["postalCode"] = customerZipTextBox.Text;
                    dictionary["country"] = customerCountryTextbox.Text;
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
    }
}
