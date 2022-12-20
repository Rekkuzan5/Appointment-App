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
    public partial class Reports : Form
    {
        int userId = 0;

        public Reports()
        {
            InitializeComponent();
            FillData();
        }
        public void FillData()
        {
            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            conn.Open();
            // Look for customers
            string query = $"SELECT userId, userName FROM user";
            MySqlDataAdapter adapt = new MySqlDataAdapter(query, conn);
            //MySqlCommand cmd = new MySqlCommand(query, conn);
            //MySqlDataReader rd = cmd.ExecuteReader();

            //    while (rd.Read())
            //    {t
            //    customerComboBox.Items.Add(rd[1]);
            //    }


            DataSet ds = new DataSet();
            adapt.Fill(ds, "Users");
            comboBox1.DisplayMember = "userName";
            comboBox1.ValueMember = "userId";
            comboBox1.DataSource = ds.Tables["Users"];
            conn.Close();

        }


        public void getTypes()
        {
            // ** Need to create appointment clas so we can make appointment objects to put into list to display. ** //
            //List<Appointment> appointments = new List<Appointment>();


            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);
            conn.Open();

            string query = $"SELECT type, start, end FROM appointment WHERE userId = '{userId}' ORDER BY MONTH(start)";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataAdapter adapt = new MySqlDataAdapter(selectCommand: cmd);

            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();


            //MySqlDataReader rdr = cmd.ExecuteReader();

            //while (rdr.Read())
            //{
            //    appointments.Add(rdr[0]);
            //}


            //months.Add("January");
            //months.Add("February");
            //months.Add("March");
            //months.Add("April");
            //months.Add("May");
            //months.Add("June");
            //months.Add("July");
            //months.Add("August");
            //months.Add("September");
            //months.Add("October");
            //months.Add("November");
            //months.Add("December");

            //MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            //List<object> monthFilter = new List<object>();


            //conn.Open();
            //// Look for customers
            //string query = $"SELECT type, SUBSTRING(start, 6, 2) FROM appointment";
            //MySqlCommand cmd = new MySqlCommand(query, conn);
            //MySqlDataReader rdr = cmd.ExecuteReader();

            //while (rdr.Read())
            //{
            //    monthFilter.Add(rdr[1]);
            //}


            //comboBox2.DisplayMember = "Value";
            //comboBox2.DataSource = new BindingSource(months, null);


            //if (monthFilter.Contains(comboBox2.SelectedItem)) {
            //    label10.Text = monthFilter.Count().ToString();
            //}
            // MySqlConnection conn = new MySqlConnection(DBConnection.Connection);


            // conn.Open();
            // Look for customers

            //string query = $"SELECT type, SUBSTRING(start, 6, 2) FROM appointment";
            //MySqlCommand cmd = new MySqlCommand(query, conn);
            // MySqlDataReader rdr = cmd.ExecuteReader();

            // while (rdr.Read())
            // {
            //     monthFilter.Add(new KeyValuePair<string, object>("type", rdr[0]));
            //     monthFilter.Add(new KeyValuePair<string, object>("start", rdr[1]));
            // }
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string month = comboBox2.Text;
            //int number = comboBox2.SelectedIndex;
            //List<KeyValuePair<string, object>> monthFilter = new List<KeyValuePair<string, object>>();

            MySqlConnection conn = new MySqlConnection(DBConnection.Connection);

            int online = 0;
            int onSite = 0;
            int phone = 0;
            conn.Open();
            // Look for customers
            string query = $"SELECT SUM(CASE WHEN type = 'Online' then 1 else 0 end) AS Internet," +
                $"sum(case when type = 'On-site' then 1 else 0 end) AS Onsite," +
                $"sum(case when type = 'Phone' then 1 else 0 end) AS Phone FROM appointment WHERE monthname(start) = '{month}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr[0] != DBNull.Value)
                {
                    online = Convert.ToInt32(rdr[0]);
                }
                else
                {
                    label10.Text = "Not Available";
                }
                if (rdr[1] != DBNull.Value)
                {
                    onSite = Convert.ToInt32(rdr[1]);
                }
                if (rdr[2] != DBNull.Value)
                {
                    phone = Convert.ToInt32(rdr[2]);
                }

                //monthFilter.Add(new KeyValuePair<string, object>("type", rdr[0]));
                //monthFilter.Add(new KeyValuePair<string, object>("start", rdr[1]));

            }
            rdr.Close();



            //int test = Convert.ToInt32(monthFilter.FirstOrDefault(kvp => kvp.Key == "start").Value) -2;
            //string type = monthFilter.FirstOrDefault(kvp => kvp.Key == "type").Value.ToString();

            //int count = monthFilter.FindAll(kvp => kvp.Key == "start").Count;




            label10.Text = online.ToString();
            label11.Text = onSite.ToString();
            label12.Text = phone.ToString();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            userId = comboBox1.SelectedIndex;
            getTypes();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
