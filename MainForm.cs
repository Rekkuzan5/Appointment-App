using Appointment_App.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appointment_App
{
    public partial class MainForm : Form
    {
        DataTable at = new DataTable();
        DateTime currentDate;


        public MainForm()
        {
            InitializeComponent();
            GetCustomers();
            RemoveAppointments();
            GetAppointments();
        }

        public static List<KeyValuePair<string, object>> CustList;

        //public static List<Customer> customers = new List<Customer>();

        public void GetCustomers()
        {

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            conn.Open();
            // Look for customers
            string query = $"SELECT customer.customerId, customer.customerName, address.address, address.phone, customer.active FROM address INNER JOIN customer ON address.addressId=customer.addressId";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter adapt = new MySqlDataAdapter(selectCommand: cmd);

            DataTable dt = new DataTable();
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
            DialogResult logout = MessageBox.Show("Logout and exit application?", "Exit Application", MessageBoxButtons.YesNo);
            if (logout == DialogResult.Yes)
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(Logic.Path), true))
                {
                    outputFile.WriteLine($"***\nUser: {Logic.CurrentUserName}\nlogged out: {Logic.getDateTime2()}\n***");
                }
                //this.Close();
                Application.Exit();
            }
        }

        // Will need more work //  need to work on next //
        private void DeleteCustomerButton_Click(object sender, EventArgs e)
        {
            if (customerDataGrid.Rows.Count > 0)
            {
                int customerId = (int)customerDataGrid.SelectedRows[0].Cells[0].Value;
                DialogResult affirmation = MessageBox.Show("Are you sure you want to delete this customer?", "Delete Customer Record", MessageBoxButtons.YesNo);
                if (affirmation == DialogResult.Yes)
                {
                    // Delete
                    CustList = Logic.SearchCustomer(customerId);
                    var list = CustList;
                    IDictionary<string, object> dictionary = list.ToDictionary(pair => pair.Key, pair => pair.Value);
                    //First we need to check if appointments exist
                    Logic.DeleteCustomer(dictionary);
                    GetCustomers();
                    MessageBox.Show("Customer successfully deleted!");
                }
                else
                {
                    MessageBox.Show("Operation Cancelled.");
                }
            }
            else
            {
                MessageBox.Show("There are no customers to delete!");
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            GetCustomers();
        }

        private void UpdateCustomerButton_Click(object sender, EventArgs e)
        {
            // pull from db to fill a customer object that can be modified and update the customer fields in the db
            int customerId = (int)customerDataGrid.SelectedRows[0].Cells[0].Value;
            UpdateCustomer update = new UpdateCustomer(customerId);
            update.Show();

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

        public void GetAppointments()
        {
            //calendar.AddBoldedDate(currentDate);
            // Look for appointments
            //MySqlDataAdapter adapt = new MySqlDataAdapter(selectCommand: cmd);
            //DataTable at = new DataTable();
            //adapt.Fill(at);
            //handleDay();
            handleWeek();
            //appointmentDataGrid.DataSource = at;
            //conn.Close();

        }

        private void RemoveAppointments()
        {
            currentDate = DateTime.Now.ToUniversalTime();
            var test = Logic.FormatUTCDateTime(currentDate);
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();
            string query = $"DELETE FROM appointment WHERE appointment.end < '{test}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlTransaction transaction = conn.BeginTransaction();

            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            transaction.Commit();


        }

        private void handleDay()
        {
            var currentDateNow = Logic.FormatUTCDateTime(currentDate);
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            calendar.RemoveAllBoldedDates();
            calendar.AddBoldedDate(currentDate);
            calendar.UpdateBoldedDates();
            //at.Clear();
            conn.Open();
            string query = $"SELECT appointment.appointmentId, customer.customerName, appointment.type, appointment.start AS Start, appointment.end AS End FROM appointment INNER JOIN customer ON appointment.customerId=customer.customerId WHERE date(start) = date('{ currentDateNow }')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            //MySqlDataAdapter adapt = new MySqlDataAdapter(selectCommand: cmd);
            //adapt.Fill(at);

            DataTable at = new DataTable();
            at.Load(cmd.ExecuteReader());
            foreach (DataRow row in at.Rows)
            {
                DateTime utcStart = Convert.ToDateTime(row["Start"]);
                DateTime utcEnd = Convert.ToDateTime(row["End"]);
                row["Start"] = TimeZone.CurrentTimeZone.ToLocalTime(utcStart);
                row["End"] = TimeZone.CurrentTimeZone.ToLocalTime(utcEnd);
            }
            appointmentDataGrid.DataSource = at;
            conn.Close();
        }

        private void handleWeek()
        {
            calendar.RemoveAllBoldedDates();
            int dow = (int)currentDate.DayOfWeek;
            string startDate = currentDate.AddDays(-dow).ToString();
            DateTime startTime = Convert.ToDateTime(startDate);
            DateTime tempDate = Convert.ToDateTime(startDate);
            for (int i = 0; i < 7; i++)
            {
                calendar.AddBoldedDate(tempDate.AddDays(i));
            }
            calendar.UpdateBoldedDates();
            string endDate = currentDate.AddDays(7 - dow).ToString();
            DateTime tempEnd = Convert.ToDateTime(endDate);
            //conn.Open();
            //string query = $"SELECT  WHERE BETWEEN date('{startDate}') AND DATE('{endDate}')";
            //MySqlCommand cmd = new MySqlCommand(query, conn);

            var currentDateNow = Logic.FormatUTCDateTime(currentDate);
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            //Logic.FormatUTCDateTime(tempDate);
            //Logic.FormatUTCDateTime(tempEnd);

            conn.Open();
            string query = $"SELECT appointment.appointmentId, customer.customerName, appointment.type, appointment.start AS Start, appointment.end AS End FROM appointment INNER JOIN customer ON appointment.customerId = customer.customerId WHERE start BETWEEN DATE('" + TimeZoneInfo.ConvertTimeToUtc(startTime).ToString("yyyy-MM-dd") + "') AND DATE('" + TimeZoneInfo.ConvertTimeToUtc(tempEnd).ToString("yyyy-MM-dd") + "');";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            //MySqlDataAdapter adapt = new MySqlDataAdapter(selectCommand: cmd);
            //adapt.Fill(at);

            DataTable at = new DataTable();
            at.Load(cmd.ExecuteReader());
            foreach (DataRow row in at.Rows)
            {
                DateTime utcStart = Convert.ToDateTime(row["Start"]);
                DateTime utcEnd = Convert.ToDateTime(row["End"]);
                row["Start"] = TimeZone.CurrentTimeZone.ToLocalTime(utcStart);
                row["End"] = TimeZone.CurrentTimeZone.ToLocalTime(utcEnd);
            }
            appointmentDataGrid.DataSource = at;
            conn.Close();

        }

        private void HandleMonth()
        {
            calendar.RemoveAllBoldedDates();
            calendar.UpdateBoldedDates();
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            //Logic.FormatUTCDateTime(tempDate);
            //Logic.FormatUTCDateTime(tempEnd);

            DateTime monthYear = DateTime.Now;
            int month = monthYear.Month;
            int year = monthYear.Year;

            conn.Open();
            string query = $"SELECT appointment.appointmentId, customer.customerName, appointment.type, appointment.start AS Start, appointment.end AS End FROM appointment INNER JOIN customer ON appointment.customerId = customer.customerId WHERE month(start) = '{month}' and year(start) = '{year}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            //MySqlDataAdapter adapt = new MySqlDataAdapter(selectCommand: cmd);
            //adapt.Fill(at);

            DataTable at = new DataTable();
            at.Load(cmd.ExecuteReader());
            foreach (DataRow row in at.Rows)
            {
                DateTime utcStart = Convert.ToDateTime(row["Start"]);
                DateTime utcEnd = Convert.ToDateTime(row["End"]);
                row["Start"] = TimeZone.CurrentTimeZone.ToLocalTime(utcStart);
                row["End"] = TimeZone.CurrentTimeZone.ToLocalTime(utcEnd);
            }
            appointmentDataGrid.DataSource = at;
            conn.Close();

        }

        private void CreateAppointmentButton_Click(object sender, EventArgs e)
        {
            AddAppointment appointment = new AddAppointment();
            appointment.Show();
        }

        private void calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            //label3.Text = calendar.SelectionStart.ToString("yyyy-MM-dd");

            currentDate = e.Start;
            //handleDay();
            //handleWeek();
            
            if (radioButton1.Checked)
            {
                HandleMonth();
            }
            if (radioButton2.Checked)
            {
                handleWeek();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (appointmentDataGrid.Rows.Count > 0)
            {
                int appointmentId = (int)appointmentDataGrid.SelectedRows[0].Cells[0].Value;
                DialogResult affirmation = MessageBox.Show("Are you sure you want to delete this appointment?", "Delete Appointment Record", MessageBoxButtons.YesNo);
                if (affirmation == DialogResult.Yes)
                {
                    // Delete

                    //First we need to check if appointments exist
                    Logic.DeleteAppointment(appointmentId);
                    GetAppointments();
                    MessageBox.Show("Appointment successfully deleted!");
                }
                else
                {
                    MessageBox.Show("Operation Cancelled.");
                }
            }
            else
            {
                MessageBox.Show("There are no appointments to delete!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (appointmentDataGrid.Rows.Count == 0)
            {
                //First we need to check if appointments exist
                MessageBox.Show("There are no appointments to delete.");
            }
            else
            {
                int appId = (int)appointmentDataGrid.SelectedRows[0].Cells[0].Value;
                UpdateAppointment update = new UpdateAppointment(appId);
                update.Show();
            }

        

            //int appId = (int)appointmentDataGrid.SelectedRows[0].Cells[0].Value;
            //UpdateAppointment update = new UpdateAppointment(appId);
            //update.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Reports reportMenu = new Reports();
            reportMenu.Show();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            handleWeek();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            HandleMonth();
        }
    }
}
