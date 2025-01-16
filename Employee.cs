using System;
using System.Collections.Generic;

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
    public string PESEL { get; private set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Role UserRole { get; set; }

    public override string ToString()
    {
        return $"{Name} {Surname} ({UserRole})";
    }


    protected static readonly Dictionary<DateTime, Employee> OnCallSchedule = new();

    public Employee(string name, string surname, string pesel, string username, string password, Role role)
    {
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

    public virtual bool AddOnCallDay(DateTime day)
    {
        if (OnCallSchedule.ContainsKey(day))
        {
            Console.WriteLine($"Error: {OnCallSchedule[day].Name} {OnCallSchedule[day].Surname} is already on call for {day:yyyy-MM-dd}.");
            return false;
        }

        OnCallSchedule[day] = this;
        Console.WriteLine($"On-call day {day:yyyy-MM-dd} assigned to {Name} {Surname}.");
        return true;
    }

    public virtual void DisplayOnCallSchedule()
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
