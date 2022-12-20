using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_App
{
    class Appointment
    {
        public static int AgentId { get; set; }
        public static int CustomerId { get; set; }
        public static string CustomerName { get; set; }
        public static DateTime AppointmentStart { get; set; }

        public Appointment(int agentId, int customerId, string customerName, DateTime appointmentStart)
        {
            AgentId = agentId;
            CustomerId = customerId;
            CustomerName = customerName;
            AppointmentStart = appointmentStart;
        }
    }
}
