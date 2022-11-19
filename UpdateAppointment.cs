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
    public partial class UpdateAppointment : Form
    {
        public int CustId { get; set; }
        public int AppointmentID { get; set; }


        //public static List<KeyValuePair<string, object>> appointmentList;

        public UpdateAppointment(int appId)
        {
            InitializeComponent();
            AppointmentID = appId;
            //FillData();
            FillFields();
        }

        //public void FillData()
        //{
        //    MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

        //    conn.Open();
        //    // Look for customers
        //    string query = $"SELECT customerId, customerName FROM customer";
        //    MySqlDataAdapter adapt = new MySqlDataAdapter(query, conn);
        //    //MySqlCommand cmd = new MySqlCommand(query, conn);
        //    //MySqlDataReader rd = cmd.ExecuteReader();

        //    //    while (rd.Read())
        //    //    {t
        //    //    customerComboBox.Items.Add(rd[1]);
        //    //    }


        //    DataSet ds = new DataSet();
        //    adapt.Fill(ds, "Customers");
        //    customerComboBox.DisplayMember = "customerName";
        //    customerComboBox.ValueMember = "customerId";
        //    customerComboBox.DataSource = ds.Tables["Customers"];

        //    conn.Close();

        //}

        public void FillFields()
        {
            var appointmentList = new List<KeyValuePair<string, object>>();

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            string query = $"SELECT appointment.customerId, appointment.title, appointment.type, appointment.start AS Start, appointment.end AS End FROM appointment INNER JOIN customer ON appointment.customerId=customer.customerId WHERE appointmentId = '{ AppointmentID }'";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                //appointmentList.Add(new KeyValuePair<string, object>("appointmentId", rdr[0]));
                appointmentList.Add(new KeyValuePair<string, object>("customerId", rdr[0]));
                appointmentList.Add(new KeyValuePair<string, object>("title", rdr[1]));
                appointmentList.Add(new KeyValuePair<string, object>("type", rdr[2]));
                appointmentList.Add(new KeyValuePair<string, object>("Start", rdr[3]));
                appointmentList.Add(new KeyValuePair<string, object>("End", rdr[4]));

                rdr.Close();
            }
            else
            {
                MessageBox.Show("No appointment found with the ID: " + AppointmentID, "Please try again");
            }
            CustId = Convert.ToInt32(appointmentList.First(kvp => kvp.Key == "customerId").Value.ToString());
            titleTextBox.Text = appointmentList.First(kvp => kvp.Key == "title").Value.ToString();
            typeComboBox.Text = appointmentList.First(kvp => kvp.Key == "type").Value.ToString();
            dateTimePicker1.Text = appointmentList.First(kvp => kvp.Key == "Start").Value.ToString();
            dateTimePicker2.Text = appointmentList.First(kvp => kvp.Key == "Start").Value.ToString();
            dateTimePicker3.Text = appointmentList.First(kvp => kvp.Key == "End").Value.ToString();
        }

        private void AppointmentCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateAppointmentButton_Click(object sender, EventArgs e)
        {
            Logic.UpdateAppointment(AppointmentID, typeComboBox.Text, titleTextBox.Text, dateTimePicker2.Value, dateTimePicker3.Value);
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;
            dateTimePicker2.Value = date;
            dateTimePicker3.Value = date;
        }
    }
}
