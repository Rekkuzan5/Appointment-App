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

        List<KeyValuePair<string, object>> appointmentList = new List<KeyValuePair<string, object>>();

        public UpdateAppointment(int appId)
        {
            InitializeComponent();
            AppointmentID = appId;
            FillFields();
        }

        public void FillFields()
        {
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
            dateTimePicker2.Text = TimeZoneInfo.ConvertTimeFromUtc((DateTime)appointmentList.First(kvp => kvp.Key == "Start").Value, TimeZoneInfo.Local).ToString();
            dateTimePicker3.Text = TimeZoneInfo.ConvertTimeFromUtc((DateTime)appointmentList.First(kvp => kvp.Key == "End").Value, TimeZoneInfo.Local).ToString();
        }

        private void AppointmentCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateAppointmentButton_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
            Logic.UpdateAppointment(AppointmentID, typeComboBox.Text, titleTextBox.Text, dateTimePicker2.Value, dateTimePicker3.Value);
            this.Close();
            }
        }

        private bool ValidateFields()
        {
            bool result = false;
            if (dateTimePicker2.Value < dateTimePicker3.Value && dateTimePicker2.Value != dateTimePicker3.Value)
            {
                if (string.IsNullOrEmpty(titleTextBox.Text) && typeComboBox.SelectedValue == null)
                {
                    MessageBox.Show("Appointment title and/or type cannot be blank!", "Error");
                    return result;
                }
                if (Logic.CompareAppointmentTimes(dateTimePicker2.Value, dateTimePicker3.Value))
                {
                    MessageBox.Show("Appointment added successfully!", "Success");
                    result = true;
                }
            }
            else
            {
                MessageBox.Show("Invalid appointment times entered!", "Error");
            }
            return result;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < DateTime.Today)
            {
                MessageBox.Show("Cannot select a date in the past for appointment.", "Error");
                dateTimePicker1.Value = Convert.ToDateTime(appointmentList.First(kvp => kvp.Key == "Start").Value);
            }
        }
    }
}
