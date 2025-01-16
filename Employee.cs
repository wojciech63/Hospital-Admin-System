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
        private string _pesel;
        public string PESEL
        {
            get { return _pesel; }
            private set
            {
                if (value.Length == 11 && value.All(char.IsDigit))
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

        public static readonly Dictionary<DateTime, Employee> OnCallSchedule = new();

        public Employee(string name, string surname, string pesel, string username, string password, Role userRole)
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

        public virtual bool AddOnCallDay(DateTime day)
        {
            if (OnCallSchedule.ContainsKey(day))
            {
                Console.WriteLine($"Error: {OnCallSchedule[day].Name} {OnCallSchedule[day].Surname} is already on call for {day:yyy-MM-dd}.");
                return false;
            }
            OnCallSchedule[day] = this;
            Console.WriteLine($"On-call day {day:yyyy-MM-dd} assigned to {Name} {Surname}");
            return true;
        }
    }
}
