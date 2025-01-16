using System;
using System.Collections.Generic;
using System.Linq;

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

    public Doctor(string name, string surname, string pesel, string username, string password, Specialization specialty, string pwz)
        : base(name, surname, pesel, username, password, Role.Doctor)
    {
        Specialty = specialty;
        PWZ = pwz;
    }

    public override bool AddOnCallDay(DateTime day)
    {
        var conflict = OnCallSchedule
            .Any(entry => entry.Key == day && entry.Value is Doctor otherDoctor && otherDoctor.Specialty == this.Specialty);

        if (conflict)
        {
            Console.WriteLine($"Error: A {Specialty} is already on-call on {day:yyyy-MM-dd}.");
            return false;
        }

        int daysThisMonth = OnCallSchedule
            .Where(entry => entry.Value == this && entry.Key.Month == day.Month && entry.Key.Year == day.Year)
            .Count();

        if (daysThisMonth >= 10)
        {
            Console.WriteLine($"Error: {Name} {Surname} cannot have more than 10 on-call days in {day:yyyy-MM}.");
            return false;
        }

        return base.AddOnCallDay(day);
    }
}
