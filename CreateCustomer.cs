using Appointment_App.Database;
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
        }

        private void CreateCustomer_Load(object sender, EventArgs e)
        {

        }

        private void CancelCustomerButton_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void CreateCustomerButton_Click(object sender, EventArgs e)
        {
            //DateTime timestamp = Logic.GetDateTime();
            //string username = Logic.CurrentUserName;

            if (string.IsNullOrEmpty(customerNameTextBox.Text) ||
                string.IsNullOrEmpty(customerAddressTextBox.Text) ||
                string.IsNullOrEmpty(customerPhoneTextBox.Text) ||
                string.IsNullOrEmpty(customerZipTextBox.Text) ||
                string.IsNullOrEmpty(listBox1.Text) ||
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
                int cityID = Logic.CreateCity(countryID, listBox1.Text);
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
