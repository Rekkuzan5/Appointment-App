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
            MainForm mainForm = new MainForm();
            mainForm.Show();
            Close();
        }

        private void CreateCustomerButton_Click(object sender, EventArgs e)
        {
            DateTime timestamp = Logic.GetDateTime();
            string username = Logic.CurrentUserName;

            //Create country table record

            //Create city table record

            //Create address table record

            //Create customer record
        }
    }
}
