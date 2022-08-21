﻿using Appointment_App.Database;
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
            //    {
            //    customerComboBox.Items.Add(rd[1]);
            //    }


            DataSet ds = new DataSet();
            adapt.Fill(ds, "Customers");
            customerComboBox.DisplayMember = "customerName";
            customerComboBox.ValueMember = "customerId";
            customerComboBox.DataSource = ds.Tables["Customers"];
            conn.Close();

            //CustId = Convert.ToInt32(customerComboBox.SelectedValue);
        }

        public void getTimes()
        {
            var pickedDate = dateTimePicker1.Value.ToShortDateString();


        }

        private void AppointmentCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateAppointmentButton_Click(object sender, EventArgs e)
        {
            Logic.createAppointment((int)customerComboBox.SelectedValue, titleTextBox.Text, descriptionTextBox.Text, LocComboBox.Text, contactTextBox.Text, typeComboBox.Text, dateTimePicker2.Value, dateTimePicker3.Value );
            this.Close();
        }
    }
}
