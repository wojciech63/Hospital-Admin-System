using Hospital_Admin_System;
using System;
using System.Collections.Generic;
using static Employee;

public class EmployeeManager
{

    public static void Main()
    {
        
        employees.Add(new Doctor("John", "Doe", "12345678901", "doctor1", "password", Doctor.Specialization.Cardiologist, "1234567"));
        employees.Add(new Nurse("Jane", "Smith", "23456789012", "nurse1", "password", Employee.Role.Nurse));
        employees.Add(new Administrator("Admin", "Admin", "34567890123", "admin", "adminpassword", Employee.Role.Administrator));

        DisplayDoctorsAndNurses();

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
    private static void ReadDate()
    {
        Console.Write("Enter a date (yyyy-MM-dd): ");
        string input = Console.ReadLine();

        if (DateTime.TryParse(input, out DateTime date))
        {
            Console.WriteLine($"The entered date is: {date:yyyy-MM-dd}");
        }
        else
        {
            Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format.");
        }
    }
    
    private static void DisplayDoctorsAndNurses()
    {
        Console.Clear();
        Console.WriteLine("List of Doctors and Nurses:");

        foreach (var employee in employees)
        {
            if (employee.UserRole != Employee.Role.Administrator) // Skip administrators
            {
                Console.WriteLine($"ID: {employee.ID} | {employee.Name} ({employee.UserRole}) ({employee.Username})");
            }
        }

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }

    public static bool AssignOnCallDayByID(List<Employee> employees, int id, DateTime day)
    {
        var employee = employees.FirstOrDefault(e => e.ID == id);

        if (employee == null)
        {
            Console.WriteLine($"Error: Employee with ID {id} not found.");
            return false;
        }

        if (employee.UserRole == Employee.Role.Doctor)
        {
            if (employee is Doctor doctor)
            {
                if (doctor.AddOnCallDay(day))
                {
                    doctor.DisplayOnCallSchedule();
                }
            }
        }

        if (employee.UserRole == Employee.Role.Nurse)
        {
            if (employee is Nurse nurse)
            {
                nurse.DisplayOnCallSchedule();
            }
        }

        
        return employee.AddOnCallDay(day);
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
            Console.WriteLine("3. Manage On Call Schedule");
            Console.WriteLine("4. Logout");
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
                ManageOnCallSchedule();
            }
            else if (choice == "4")
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

    private static void ManageOnCallSchedule()
    {
        Console.Clear();
        Console.Clear();
        Console.WriteLine("Manage On-Call Schedule");
        Console.WriteLine("1. Add On-Call Day");
        Console.WriteLine("2. Remove On-Call Day");
        Console.Write("Choose an option: ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.WriteLine("Which employee do you choose?");
            DisplayDoctorsAndNurses();
            Console.Write("Enter Employee ID to assign on-call day: ");
            if (int.TryParse(Console.ReadLine(), out int employeeID))
            {
                Console.Write("Enter On-Call Date (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime onCallDate))
                {
                    bool result = AssignOnCallDayByID(employees, employeeID, onCallDate);
                    if (!result)
                    {
                        Console.WriteLine("Failed to assign on-call day.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter the date as yyyy-MM-dd.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Employee ID. Please enter a numeric value.");
            }
        }
    }
}
