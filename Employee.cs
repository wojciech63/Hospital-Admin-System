using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Admin_System
{
    public abstract class Employee
    {
        public enum Role
        {
            Doctor,
            Nurse,
            Administrator
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        private int _pesel;
        public int PESEL
        {
            get { return _pesel; }
            private set
            {
                if (value.ToString().Length == 11)
                {
                    _pesel = value;
                }
                else
                {
                    throw new ArgumentException("PESEL must be an 11-digit number.");
                }
            }
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }

        protected static readonly Dictionary<DateTime, Employee> OnCallSchedule = new();

        public Employee(string name, string surname, int pesel, string username, string password, Role userRole)
        {
            Name = name;
            Surname = surname;
            PESEL = pesel;
            Username = username;
            Password = password;
            UserRole = userRole;
        }

        public override string ToString()
        {
            return Name + " " + Surname + "(" + Username + ")";
        }

        public bool Login(string username, string password)
        {
            return this.Password == password && this.Username == username;
        }

    }
}
