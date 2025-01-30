using System;
using System.Linq;
using HospitalSystem;

namespace HospitalSystem
{
    public class Doctor : Employee
    {
        public Specialization Specialty { get; set; }
        private string _pwz;
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
                    throw new ArgumentException("PWZ must be exactly 7 digits (0-9).");
                }
            }
        }

        public Doctor(string name, string surname, string pesel, string username, string password,
                      Specialization specialty, string pwz)
            : base(name, surname, pesel, username, password, Role.Doctor)
        {
            Specialty = specialty;
            PWZ = pwz;
        }

        public override bool AddOnCallDay(DateTime date)
        {
            if (GetAssignedDaysThisMonth(date) >= 10)
            {
                Console.WriteLine($"Doctor {Name} {Surname} already has 10 on-call days in {date:yyyy-MM}.");
                return false;
            }

            if (IsConsecutiveDay(date))
            {
                Console.WriteLine($"Doctor {Name} {Surname} cannot have consecutive on-call days around {date:yyyy-MM-dd}.");
                return false;
            }

            if (!OnCallSchedule.TryGetValue(date, out var employeesThatDay))
            {
                employeesThatDay = new System.Collections.Generic.List<Employee>();
                OnCallSchedule[date] = employeesThatDay;
            }
            bool sameSpecialtyExists = employeesThatDay
                .OfType<Doctor>()
                .Any(d => d.Specialty == this.Specialty);
            if (sameSpecialtyExists)
            {
                Console.WriteLine($"Another {Specialty} is already on-call on {date:yyyy-MM-dd}.");
                return false;
            }

            employeesThatDay.Add(this);
            Console.WriteLine($"Assigned Doctor {Name} {Surname} ({Specialty}) on {date:yyyy-MM-dd}.");
            return true;
        }

        public override string ToString()
        {
            return base.ToString() + $" [Spec: {Specialty}, PWZ: {PWZ}]";
        }
    }
}
