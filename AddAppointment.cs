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
    public partial class AddAppointment : Form
    {
        public AddAppointment()
        {
            InitializeComponent();
            FillData();
        }

        public static List<int> apptTimes = new List<int>();

        public void FillData()
        {
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            conn.Open();
            // Look for customers
            string query = $"SELECT customer.customerId, customer.customerName FROM customer";
            MySqlDataAdapter adapt = new MySqlDataAdapter(query, conn);

            DataSet ds = new DataSet();
            adapt.Fill(ds, "Customers");
            customerComboBox.DisplayMember = "customerName";
            customerComboBox.ValueMember = "customerId";
            customerComboBox.DataSource = ds.Tables["Customers"];
            conn.Close();

            apptTimes.Add(32);
            apptTimes.Add(21);
            apptTimes.Add(45);
            apptTimes.Add(11);
            apptTimes.Add(89);
        }

        public void getTimes()
        {
            var pickedDate = dateTimePicker1.Value.ToShortDateString();


        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void descLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
