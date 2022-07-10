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
    public partial class MainForm : Form
    {
        DataTable dt = new DataTable();

        public MainForm()
        {
            InitializeComponent();
            customerDataGrid.Columns[0].Width = 50;
            GetCustomers();
        }


        //public static List<Customer> customers = new List<Customer>();

        public void GetCustomers()
        {

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            conn.Open();
            // Look for customers
            string query = $"SELECT customer.customerId, customer.customerName, address.address, address.phone, customer.active FROM address INNER JOIN customer ON address.addressId=customer.addressId";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter adapt = new MySqlDataAdapter(selectCommand: cmd);

            adapt.Fill(dt);
            customerDataGrid.DataSource = dt;
            conn.Close();

        }

        private void addCustomerButton_Click(object sender, EventArgs e)
        {
            CreateCustomer create = new CreateCustomer();
            create.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
            
        }

        private void DeleteCustomerButton_Click(object sender, EventArgs e)
        {
            /*DialogResult affirmation =*/ MessageBox.Show("Are you sure you want to delete this customer?", "This cannot be reversed!", MessageBoxButtons.YesNo);

        }

        //static public Array getCalendar(bool weekView)
        //{
        //    MySqlConnection c = new MySqlConnection(DBConnection.Connection);
        //    c.Open();
        //    // Queries the DB for all the appointments related to the logged in user
        //    string query = $"SELECT customerId, type, start, end, appointmentId, userId FROM appointment WHERE userid = '{Logic.CurrentUserID}'";
        //    MySqlCommand cmd = new MySqlCommand(query, c);
        //    MySqlDataReader rdr = cmd.ExecuteReader();

        //    Dictionary<int, Hashtable> appointments = new Dictionary<int, Hashtable>();

        //    // Creates a dictionary of all the appointments
        //    while (rdr.Read())
        //    {

        //        Hashtable appointment = new Hashtable();
        //        appointment.Add("customerId", rdr[0]);
        //        appointment.Add("type", rdr[1]);
        //        appointment.Add("start", rdr[2]);
        //        appointment.Add("end", rdr[3]);
        //        appointment.Add("userId", rdr[5]);

        //        appointments.Add(Convert.ToInt32(rdr[4]), appointment);

        //    }
        //    rdr.Close();
    }
}
