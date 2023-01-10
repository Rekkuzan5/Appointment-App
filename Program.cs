using Appointment_App.Database;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appointment_App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //DBConnection.StartConnection();
            Application.Run(new LoginForm());
            //DBConnection.CloseConnection();

            // test for language change on login form

            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            //Application.Run(new LoginForm());
        }
    }
}
