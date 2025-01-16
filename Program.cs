using System;
using System.Collections.Generic;

namespace Hospital_Admin_System
{
    class Program
    {
        static void Main(string[] args)
        {
            var doctor1 = new Doctor("John", "Doe", "12345678901", "jdoe", "password123", Doctor.Specialization.Cardiologist, "1234567", Employee.Role.Doctor);
            var doctor2 = new Doctor("Jane", "Smith", "98765432101", "jsmith", "password456", Doctor.Specialization.Cardiologist, "7654321", Employee.Role.Doctor);
            var doctor3 = new Doctor("Emily", "Brown", "56789012345", "ebrown", "password789", Doctor.Specialization.Neurologist, "2345678", Employee.Role.Doctor);

            Console.WriteLine("Adding on-call days...");
            var date1 = new DateTime(2025, 1, 15);
            var date2 = new DateTime(2025, 1, 16);

            doctor1.AddOnCallDay(date1);
            doctor2.AddOnCallDay(date1);
            doctor3.AddOnCallDay(date1); 

            doctor1.AddOnCallDay(date2); 

            Console.WriteLine("\nDisplaying on-call schedule:");
            foreach (var entry in Employee.OnCallSchedule)
            {
                Console.WriteLine($"{entry.Key:yyyy-MM-dd}: {entry.Value.Name} {entry.Value.Surname} ({entry.Value.UserRole})");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
asdawasda