using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_App
{
    class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public int CustomerPostalCode { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerCountry { get; set; }
        public bool IsActive { get; set; }

        public static BindingList<Customer> customers = new BindingList<Customer>();

        public Customer(int id, string name, string address, string phone, int postalCode, string city, string country, bool isActive)
        {
            CustomerID = id;
            CustomerName = name;
            CustomerAddress = address;
            CustomerPhone = phone;
            CustomerPostalCode = postalCode;
            CustomerCity = city;
            CustomerCountry = country;
            IsActive = isActive;
        }

    }
}
