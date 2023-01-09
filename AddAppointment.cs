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
        //private string _selectedDate;
        public int CustId { get; set; }

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
            string query = $"SELECT customerId, customerName FROM customer";
            MySqlDataAdapter adapt = new MySqlDataAdapter(query, conn);
            //MySqlCommand cmd = new MySqlCommand(query, conn);
            //MySqlDataReader rd = cmd.ExecuteReader();

            //    while (rd.Read())
            //    {t
            //    customerComboBox.Items.Add(rd[1]);
            //    }


            DataSet ds = new DataSet();
            adapt.Fill(ds, "Customers");
            customerComboBox.DisplayMember = "customerName";
            customerComboBox.ValueMember = "customerId";
            customerComboBox.DataSource = ds.Tables["Customers"];
            conn.Close();

        }

        public void getTimes()
        {
            //var initialDate = selectedDate;


        }

        private void AppointmentCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateAppointmentButton_Click(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value < dateTimePicker3.Value && dateTimePicker2.Value != dateTimePicker3.Value)
            {
                if (Logic.CompareAppointmentTimes(dateTimePicker2.Value, dateTimePicker3.Value))
                {
                    MessageBox.Show("Appointment added successfully!");
                    Logic.createAppointment((int)customerComboBox.SelectedValue, titleTextBox.Text, typeComboBox.Text, dateTimePicker2.Value, dateTimePicker3.Value);
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Operation Failed");
                }
            }
            else
            {
                MessageBox.Show("Invalid appointment times entered!");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;
            dateTimePicker2.Value = date;
            dateTimePicker3.Value = date;
        }
    }
}
