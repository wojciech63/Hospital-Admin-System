using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Admin_System
{
    public class Doctor : Employee
    {
        public enum Specialization
        {
            Cardiologist,
            Urologist,
            Neurologist,
            Laryngologist
        }

        public Specialization Specialty { get; set; }
        private string _pwz;
        private readonly Dictionary<int, List<DateTime>> _onCallSchedule = new();

        public string PWZ
        {
            get => _pwz;
            set
            {
                if (value.Length == 7 && value.All(char.IsDigit))
                {
                    _pwz = value;
                }
                else
                {
                    throw new ArgumentException("PWZ must be 7 digits long.");
                }
            }
        }

        public Doctor(string name, string surname, string pesel, string username, string password, Specialization specialty, string pwz, Role role)
            : base(name, surname, pesel, username, password, role)
        {
            Specialty = specialty;
            PWZ = pwz;
        }

        public override bool AddOnCallDay(DateTime day)
        {
            var conflict = OnCallSchedule.Any(entry => entry.Key == day && entry.Value is Doctor doc && doc.Specialty == this.Specialty);

            if (conflict)
            {
                Console.WriteLine($"Error: A {Specialty} is already on-call on {day:yyyy-MM-dd}.");
                return false;
            }

            return base.AddOnCallDay(day);
        }
    }
}
