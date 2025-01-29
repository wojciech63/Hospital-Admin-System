using System;
using System.Collections.Generic;

public abstract class Employee
{
    private static List<Employee> employees = new List<Employee>();
    public enum Role
    {
        Doctor,
        Nurse,
        Administrator
    }

    public int ID { get; private set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PESEL { get; private set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Role UserRole { get; set; }

    public override string ToString()
    {
        return $"ID: {ID} | {Name} {Surname} ({UserRole})";
    }


    protected static readonly Dictionary<DateTime, Doctor> OnCallSchedule = new();
    private static int _nextID = 1;
    public Employee(string name, string surname, string pesel, string username, string password, Role role)
    {
        ID = _nextID++;
        Name = name;
        Surname = surname;
        Username = username;
        Password = password;
        UserRole = role;

        if (pesel.Length != 11 || !long.TryParse(pesel, out _))
        {
            throw new ArgumentException("PESEL must be an 11-digit number.");
        }
        PESEL = pesel;
    }

    public virtual bool AddOnCallDayByID(List <Employee> employees, int id, DateTime day)
    {

        var employee = employees.FirstOrDefault(e => e.ID == id);

        if (employee == null)
        {
            Console.WriteLine($"Error: Employee with ID: {id} note found");
            return false;
        }

        if (OnCallSchedule.TryGetValue(day, out Doctor assignedEmployee))
        {
            if (assignedEmployee is Employee assignedDoctor && assignedDoctor.UserRole == Employee.Role.Doctor)
            {

            }
        }

        if (OnCallSchedule.ContainsKey(day))
        {
            Console.WriteLine($"Error: {OnCallSchedule[day].Name} {OnCallSchedule[day].Surname} is already on call for {day:yyyy-MM-dd}.");
            return false;
        }

        OnCallSchedule[day] = this;
        Console.WriteLine($"On-call day {day:yyyy-MM-dd} assigned to {Name} {Surname}.");
        return true;
    }

    public void DisplayOnCallSchedule()
    {
        Console.WriteLine($"On-call schedule for {Name} {Surname}:");
        foreach (var entry in OnCallSchedule)
        {
            if (entry.Value == this)
            {
                Console.WriteLine($"- {entry.Key:yyyy-MM-dd}");
            }
        }
    }
    
}
