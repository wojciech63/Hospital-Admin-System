using System;
using System.Linq;
using HospitalSystem;

namespace HospitalSystem
{
    public class Nurse : Employee
    {
        public Nurse(string name, string surname, string pesel, string username, string password)
            : base(name, surname, pesel, username, password, Role.Nurse)
        {
        }

        public override bool AddOnCallDay(DateTime date)
        {
            if (GetAssignedDaysThisMonth(date) >= 10)
            {
                Console.WriteLine($"Nurse {Name} {Surname} already has 10 on-call days in {date:yyyy-MM}.");
                return false;
            }

            if (IsConsecutiveDay(date))
            {
                Console.WriteLine($"Nurse {Name} {Surname} cannot have consecutive on-call days around {date:yyyy-MM-dd}.");
                return false;
            }

            if (!OnCallSchedule.TryGetValue(date, out var employeesThatDay))
            {
                employeesThatDay = new System.Collections.Generic.List<Employee>();
                OnCallSchedule[date] = employeesThatDay;
            }
            bool nurseExists = employeesThatDay.OfType<Nurse>().Any();
            if (nurseExists)
            {
                Console.WriteLine($"Another nurse is already on-call on {date:yyyy-MM-dd}.");
                return false;
            }

            employeesThatDay.Add(this);
            Console.WriteLine($"Assigned Nurse {Name} {Surname} on {date:yyyy-MM-dd}.");
            return true;
        }
    }
}
