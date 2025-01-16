using Hospital_Admin_System;
using System;
using System.Collections.Generic;

public class Program
{
    private static List<Employee> employees = new List<Employee>();

    public static void Main()
    {
        employees.Add(new Doctor("John", "Doe", "12345678901", "doctor1", "password", Doctor.Specialization.Cardiologist, "1234567"));
        employees.Add(new Nurse("Jane", "Smith", "23456789012", "nurse1", "password", Employee.Role.Nurse));
        employees.Add(new Administrator("Admin", "Admin", "34567890123", "admin", "adminpassword", Employee.Role.Administrator));

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Hospital System");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Login();
            }
            else if (choice == "2")
            {
                Console.WriteLine("Exiting the system. Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Press any key to try again...");
                Console.ReadKey();
            }
        }
    }

    private static void Login()
    {
        Console.Clear();
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        Employee loggedInUser = employees.Find(e => e.Username == username && e.Password == password);

        if (loggedInUser != null)
        {
            switch (loggedInUser.UserRole)
            {
                case Employee.Role.Doctor:
                    DoctorMenu((Doctor)loggedInUser);
                    break;
                case Employee.Role.Nurse:
                    NurseMenu((Nurse)loggedInUser);
                    break;
                case Employee.Role.Administrator:
                    AdminMenu();
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid username or password. Press any key to return...");
            Console.ReadKey();
        }
    }

    private static void DoctorMenu(Doctor doctor)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Welcome, Dr. {doctor.Name} {doctor.Surname}");
            Console.WriteLine("1. View On-Call Schedule");
            Console.WriteLine("2. View Colleagues");
            Console.WriteLine("3. Logout");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Clear();
                doctor.DisplayOnCallSchedule();
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
            }
            else if (choice == "2")
            {
                Console.Clear();
                Console.WriteLine("Colleagues:");
                foreach (var emp in employees)
                {
                    if (emp is Doctor doc)
                        Console.WriteLine($"Dr. {doc.Name} {doc.Surname} - {doc.Specialty}");
                    else if (emp is Nurse nurse)
                        Console.WriteLine($"Nurse: {nurse.Name} {nurse.Surname}");
                }
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
            }
            else if (choice == "3")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Try again.");
            }
        }
    }

    private static void NurseMenu(Nurse nurse)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Welcome, Nurse {nurse.Name} {nurse.Surname}");
            Console.WriteLine("1. View On-Call Schedule");
            Console.WriteLine("2. View Colleagues");
            Console.WriteLine("3. Logout");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Clear();
                nurse.DisplayOnCallSchedule();
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
            }
            else if (choice == "2")
            {
                Console.Clear();
                Console.WriteLine("Colleagues:");
                foreach (var emp in employees)
                {
                    if (emp is Doctor doc)
                        Console.WriteLine($"Dr. {doc.Name} {doc.Surname} - {doc.Specialty}");
                    else if (emp is Nurse nur)
                        Console.WriteLine($"Nurse: {nur.Name} {nur.Surname}");
                }
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
            }
            else if (choice == "3")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Try again.");
            }
        }
    }

    private static void AdminMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Administrator Menu");
            Console.WriteLine("1. View All Employees");
            Console.WriteLine("2. Add New Employee");
            Console.WriteLine("3. Logout");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Clear();
                Console.WriteLine("All Employees:");
                foreach (var emp in employees)
                {
                    Console.WriteLine(emp);
                }
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
            }
            else if (choice == "2")
            {
                AddNewEmployee();
            }
            else if (choice == "3")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Try again.");
            }
        }
    }

    private static void AddNewEmployee()
    {
        Console.Clear();
        Console.WriteLine("Add New Employee:");
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Surname: ");
        string surname = Console.ReadLine();

        Console.Write("Enter PESEL: ");
        string pesel = Console.ReadLine();

        Console.Write("Enter Username: ");
        string username = Console.ReadLine();

        Console.Write("Enter Password: ");
        string password = Console.ReadLine();

        Console.WriteLine("Select Role: 1. Doctor, 2. Nurse, 3. Administrator");
        Employee.Role role = (Employee.Role)(int.Parse(Console.ReadLine()) - 1);

        if (role == Employee.Role.Doctor)
        {
            Console.WriteLine("Choose Specialization: 1. Cardiologist, 2. Urologist, 3. Neurologist, 4. Laryngologist");
            var specialty = (Doctor.Specialization)(int.Parse(Console.ReadLine()) - 1);

            Console.Write("Enter PWZ (7 digits): ");
            string pwz = Console.ReadLine();

            employees.Add(new Doctor(name, surname, pesel, username, password, specialty, pwz));
        }
        else if (role == Employee.Role.Nurse)
        {
            employees.Add(new Nurse(name, surname, pesel, username, password, role));
        }
        else if (role == Employee.Role.Administrator)
        {
            employees.Add(new Administrator(name, surname, pesel, username, password, role));
        }

        Console.WriteLine("Employee added successfully. Press any key to return...");
        Console.ReadKey();
    }
}
