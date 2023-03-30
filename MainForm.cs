using Appointment_App.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
            //RemoveAppointments();
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

        // Search function for searching customers on main menu
        private void SearchMember()
        {
            if (!string.IsNullOrEmpty(searchMemberBox.Text))
            {
                foreach (DataGridViewRow row in customerDataGrid.Rows)
                {
                    if (row.Cells["customerName"].Value.ToString().ToUpper().Contains(searchMemberBox.Text.ToUpper()))
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
        }

        private void addCustomerButton_Click(object sender, EventArgs e)
        {
            CreateCustomer create = new CreateCustomer();
            create.Show();
        }

        private void exitButton_Click(object sender, EventArgs e)
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

        public void GetAppointments()
        {
            handleWeek();
        }

        // This function is to remove the appointments that have expired automatically once their date has past.  This can be commented out to preserve appointments.
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

        // not used but could be implemented if needed.
        private void handleDay()
        {
            var currentDateNow = Logic.FormatUTCDateTime(currentDate);
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            calendar.RemoveAllBoldedDates();
            calendar.AddBoldedDate(currentDate);
            calendar.UpdateBoldedDates();

            conn.Open();
            string query = $"SELECT appointment.appointmentId, customer.customerName, appointment.type, appointment.start AS Start, appointment.end AS End FROM appointment INNER JOIN customer ON appointment.customerId=customer.customerId WHERE date(start) = date('{ currentDateNow }')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
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
            currentDate = calendar.SelectionStart;
            int dow = (int)currentDate.DayOfWeek;
            DateTime startTime = currentDate.AddDays(-dow);
            DateTime tempDate = Convert.ToDateTime(startTime);
            for (int i = 0; i < 7; i++)
            {
                calendar.AddBoldedDate(tempDate.AddDays(i));
            }
            calendar.UpdateBoldedDates();
            DateTime endDate = currentDate.AddDays(7 - dow);

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();
            string query = $"SELECT appointment.appointmentId, customer.customerName, appointment.type, appointment.start AS Start, appointment.end AS End FROM appointment INNER JOIN customer ON appointment.customerId = customer.customerId WHERE start BETWEEN DATE('" + TimeZoneInfo.ConvertTimeToUtc(startTime).ToString("yyyy-MM-dd") + "') AND DATE('" + TimeZoneInfo.ConvertTimeToUtc(endDate).ToString("yyyy-MM-dd") + "');";
            MySqlCommand cmd = new MySqlCommand(query, conn);

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
            at.Clear();
            calendar.RemoveAllBoldedDates();
            calendar.UpdateBoldedDates();
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            DateTime monthYear = calendar.SelectionStart;
            int month = monthYear.Month;
            int year = monthYear.Year;

            conn.Open();
            string query = $"SELECT appointment.appointmentId, customer.customerName, appointment.type, appointment.start AS Start, appointment.end AS End FROM appointment INNER JOIN customer ON appointment.customerId = customer.customerId WHERE month(start) = '{month}' and year(start) = '{year}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            //DataTable at = new DataTable();
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
            currentDate = e.Start;
            
            if (radioButton1.Checked)
            {
                HandleMonth();
            }
            if (radioButton2.Checked)
            {
                handleWeek();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
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

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (appointmentDataGrid.Rows.Count == 0)
            {
                //First we need to check if appointments exist
                MessageBox.Show("There are no appointments to update.");
            }
            else
            {
                int appId = (int)appointmentDataGrid.SelectedRows[0].Cells[0].Value;
                UpdateAppointment update = new UpdateAppointment(appId);
                update.Show();
            }
        }

        private void reportsButton_Click(object sender, EventArgs e)
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

		private void button1_Click(object sender, EventArgs e)
		{
            SearchMember();
		}
	}
}
