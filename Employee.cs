using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HospitalSystem;

namespace HospitalSystem
{
    public abstract class Employee
    {
        public static Dictionary<DateTime, List<Employee>> OnCallSchedule { get; private set; }
            = new Dictionary<DateTime, List<Employee>>();

        private static int _nextID = 1;

        public int ID { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PESEL { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }

        protected Employee(string name, string surname, string pesel, string username, string password, Role role)
        {
            ID = _nextID++;
            Name = name;
            Surname = surname;
            Username = username;
            Password = password;
            UserRole = role;

            if (pesel.Length != 11 || !long.TryParse(pesel, out _))
                throw new ArgumentException("PESEL must be an 11-digit number.");

            PESEL = pesel;
        }

        public override string ToString()
        {
            return $"ID: {ID} | {Name} {Surname} ({UserRole})";
        }

        public virtual bool AddOnCallDay(DateTime date)
        {
            Console.WriteLine("This role does not have on-call duties.");
            return false;
        }

        public virtual bool RemoveOnCallDay(DateTime date)
        {
            if (OnCallSchedule.TryGetValue(date, out var list))
            {
                if (list.Remove(this))
                {
                    if (list.Count == 0) OnCallSchedule.Remove(date);
                    return true;
                }
            }
            return false;
        }

        public void DisplayScheduleForMonth(int month)
        {
            var assignedDays = OnCallSchedule
                .Where(kvp => kvp.Key.Month == month && kvp.Value.Contains(this))
                .Select(kvp => kvp.Key)
                .OrderBy(d => d);

            Console.WriteLine($"=== Schedule for {Name} {Surname} in month {month} ===");
            if (!assignedDays.Any())
            {
                Console.WriteLine("No assigned days.");
            }
            else
            {
                foreach (var day in assignedDays)
                {
                    Console.WriteLine($" - {day:yyyy-MM-dd}");
                }
            }
        }

        public static bool AssignOnCallDayByID(List<Employee> allEmployees, int employeeID, DateTime date)
        {
            var emp = allEmployees.FirstOrDefault(e => e.ID == employeeID);
            if (emp == null)
            {
                Console.WriteLine($"No employee with ID={employeeID} found.");
                return false;
            }
            return emp.AddOnCallDay(date);
        }

        public static bool RemoveOnCallDayByID(List<Employee> allEmployees, int employeeID, DateTime date)
        {
            var emp = allEmployees.FirstOrDefault(e => e.ID == employeeID);
            if (emp == null)
            {
                Console.WriteLine($"No employee with ID={employeeID} found.");
                return false;
            }
            return emp.RemoveOnCallDay(date);
        }

        protected int GetAssignedDaysThisMonth(DateTime date)
        {
            return OnCallSchedule
                .Where(kvp => kvp.Key.Year == date.Year && kvp.Key.Month == date.Month)
                .Count(kvp => kvp.Value.Contains(this));
        }

        protected bool IsConsecutiveDay(DateTime date)
        {
            var prev = date.AddDays(-1);
            var next = date.AddDays(1);

            if (OnCallSchedule.TryGetValue(prev, out var prevList) && prevList.Contains(this))
                return true;

            if (OnCallSchedule.TryGetValue(next, out var nextList) && nextList.Contains(this))
                return true;

            return false;
        }
    }
}
