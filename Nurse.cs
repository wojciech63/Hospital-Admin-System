using System;
using System.Xml.Linq;

public class Nurse : Employee
{
    public Nurse(string name, string surname, string pesel, string username, string password, Role role)
        : base(name, surname, pesel, username, password, role)
    {
    }

    public override bool AddOnCallDay(DateTime day)
    {
        if (OnCallSchedule.ContainsKey(day))
        {
            if (OnCallSchedule[day] is Nurse nurse && nurse != this)
            {
                Console.WriteLine($"Error: Another Nurse is already on call for {day:yyyy-MM-dd}.");
                return false;
            }
            if (OnCallSchedule[day] == this)
            {
                Console.WriteLine($"Error: {Name} {Surname} is already on call for {day:yyyy-MM-dd}.");
                return false;
            }
        }

        OnCallSchedule[day] = this;
        Console.WriteLine($"On-call day {day:yyyy-MM-dd} assigned to {Name} {Surname}.");
        return true;
    }
}
