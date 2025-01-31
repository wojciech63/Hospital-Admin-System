using HospitalSystem;
using System;
using System.Collections.Generic;
using System.Linq;


namespace HospitalSystem
{
    class Program
    {
        static List<Employee> employees = new List<Employee>();

        static void Main(string[] args)
        {
            SeedSampleData();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== HOSPITAL ADMIN SYSTEM ===");
                Console.WriteLine("1) Login");
                Console.WriteLine("2) Exit");
                Console.Write("Choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Login();
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }
            }
        }

        static void Login()
        {
            Console.Clear();
            Console.Write("Username: ");
            string user = Console.ReadLine();
            Console.Write("Password: ");
            string pass = Console.ReadLine();

            var found = employees.FirstOrDefault(e =>
                e.Username == user && e.Password == pass);

            if (found == null)
            {
                Console.WriteLine("Invalid credentials. Press any key to return...");
                Console.ReadKey();
                return;
            }

            switch (found.UserRole)
            {
                case Role.Administrator:
                    AdminMenu((Administrator)found);
                    break;
                case Role.Doctor:
                    DoctorMenu((Doctor)found);
                    break;
                case Role.Nurse:
                    NurseMenu((Nurse)found);
                    break;
            }
        }

        static void AdminMenu(Administrator admin)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== ADMIN MENU: {admin.Name} ===");
                Console.WriteLine("1) View All Employees");
                Console.WriteLine("2) Edit Employee Data");
                Console.WriteLine("3) Add New Employee");
                Console.WriteLine("4) Manage On-Call (Add/Remove)");
                Console.WriteLine("5) Logout");
                Console.Write("Choice: ");
                string ch = Console.ReadLine();

                if (ch == "1") ShowAllEmployees();
                else if (ch == "2") EditEmployeeData();
                else if (ch == "3") AddEmployee();
                else if (ch == "4") ManageOnCall();
                else if (ch == "5") break;
            }
        }

        static void DoctorMenu(Doctor doc)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== DOCTOR MENU: {doc.Name} {doc.Surname} ===");
                Console.WriteLine("1) View Doctors and Nurses");
                Console.WriteLine("2) View Person's Schedule for a Month");
                Console.WriteLine("3) Logout");
                Console.Write("Choice: ");
                string ch = Console.ReadLine();

                if (ch == "1") ShowDoctorsAndNurses();
                else if (ch == "2") ShowScheduleForPerson();
                else if (ch == "3") break;
            }
        }

        static void NurseMenu(Nurse nur)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== NURSE MENU: {nur.Name} {nur.Surname} ===");
                Console.WriteLine("1) View Doctors and Nurses");
                Console.WriteLine("2) View Person's Schedule for a Month");
                Console.WriteLine("3) Logout");
                Console.Write("Choice: ");
                string ch = Console.ReadLine();

                if (ch == "1") ShowDoctorsAndNurses();
                else if (ch == "2") ShowScheduleForPerson();
                else if (ch == "3") break;
            }
        }

        static void ShowAllEmployees()
        {
            Console.Clear();
            Console.WriteLine("=== ALL EMPLOYEES ===");
            foreach (var e in employees)
            {
                Console.WriteLine(e);
            }
            Pause();
        }

        static void EditEmployeeData()
        {
            Console.Clear();
            Console.Write("Enter Employee ID to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var emp = employees.FirstOrDefault(e => e.ID == id);
            if (emp == null)
            {
                Console.WriteLine($"No employee with ID={id}.");
                Pause();
                return;
            }

            Console.Write($"New Name (leave blank to keep '{emp.Name}'): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) emp.Name = newName;

            Console.Write($"New Surname (leave blank to keep '{emp.Surname}'): ");
            string newSurname = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSurname)) emp.Surname = newSurname;

            Console.Write($"New Username (leave blank to keep '{emp.Username}'): ");
            string newUser = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newUser)) emp.Username = newUser;

            Console.Write($"New Password (leave blank to keep current): ");
            string newPass = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPass)) emp.Password = newPass;

            if (emp is Doctor doc)
            {
                Console.WriteLine("Change Specialty? (1=Cardio, 2=Uro, 3=Neuro, 4=Lary), or blank to skip:");
                string specChoice = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(specChoice))
                {
                    if (specChoice == "1") doc.Specialty = Specialization.Cardiologist;
                    if (specChoice == "2") doc.Specialty = Specialization.Urologist;
                    if (specChoice == "3") doc.Specialty = Specialization.Neurologist;
                    if (specChoice == "4") doc.Specialty = Specialization.Laryngologist;
                }

                Console.Write($"New PWZ (7 digits, blank to skip): ");
                string newPwz = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newPwz))
                {
                    try
                    {
                        doc.PWZ = newPwz;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error setting PWZ: " + ex.Message);
                    }
                }
            }


            Console.WriteLine("Data updated.");
            Pause();
        }

        static void AddEmployee()
        {
            Console.Clear();
            Console.WriteLine("=== ADD NEW EMPLOYEE ===");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Surname: ");
            string sur = Console.ReadLine();
            Console.Write("PESEL (11 digits): ");
            string pesel = Console.ReadLine();
            Console.Write("Username: ");
            string user = Console.ReadLine();
            Console.Write("Password: ");
            string pass = Console.ReadLine();

            Console.WriteLine("Role? 1=Admin, 2=Doctor, 3=Nurse:");
            string roleChoice = Console.ReadLine();
            if (roleChoice == "1")
            {
                employees.Add(new Administrator(name, sur, pesel, user, pass));
            }
            else if (roleChoice == "2")
            {
                Console.WriteLine("Specialty? 1=Cardio, 2=Uro, 3=Neuro, 4=Lary:");
                string specNum = Console.ReadLine();
                Specialization s = Specialization.Cardiologist;
                if (specNum == "2") s = Specialization.Urologist;
                if (specNum == "3") s = Specialization.Neurologist;
                if (specNum == "4") s = Specialization.Laryngologist;

                Console.Write("PWZ (7 digits): ");
                string pwzVal = Console.ReadLine();

                try
                {
                    employees.Add(new Doctor(name, sur, pesel, user, pass, s, pwzVal));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error creating doctor: " + ex.Message);
                }
            }
            else if (roleChoice == "3")
            {
                employees.Add(new Nurse(name, sur, pesel, user, pass));
            }
            else
            {
                Console.WriteLine("Unknown role. Cancelled.");
            }

            Console.WriteLine("Employee added (if no error).");
            Pause();
        }

        static void ManageOnCall()
        {
            Console.Clear();
            Console.WriteLine("1) Add On-Call Day");
            Console.WriteLine("2) Remove On-Call Day");
            Console.Write("Choice: ");
            string c = Console.ReadLine();

            Console.Write("Employee ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }
            Console.Write("Date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Invalid date format.");
                Pause();
                return;
            }

            if (c == "1")
            {
                Employee.AssignOnCallDayByID(employees, id, date);
            }
            else if (c == "2")
            {
                bool ok = Employee.RemoveOnCallDayByID(employees, id, date);
                if (ok) Console.WriteLine("Removed on-call assignment.");
                else Console.WriteLine("Could not remove (not assigned?).");
            }

            Pause();
        }


        static void ShowDoctorsAndNurses()
        {
            Console.Clear();
            Console.WriteLine("=== DOCTORS & NURSES ===");
            foreach (var e in employees)
            {
                if (e.UserRole == Role.Doctor || e.UserRole == Role.Nurse)
                {
                    Console.WriteLine(e);
                }
            }
            Pause();
        }

        static void ShowScheduleForPerson()
        {
            Console.Clear();
            Console.Write("Enter username: ");
            string uname = Console.ReadLine();

            var found = employees.FirstOrDefault(e => e.Username == uname &&
                                 (e.UserRole == Role.Doctor || e.UserRole == Role.Nurse));
            if (found == null)
            {
                Console.WriteLine("No such doctor/nurse found.");
                Pause();
                return;
            }

            Console.Write("Which month (1-12)? ");
            if (!int.TryParse(Console.ReadLine(), out int m) || m < 1 || m > 12)
            {
                Console.WriteLine("Invalid month.");
                Pause();
                return;
            }

            found.DisplayScheduleForMonth(m);
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void SeedSampleData()
        {
            employees.Add(new Administrator("Admin", "Admin", "11111111111", "admin", "admin123"));
            employees.Add(new Doctor("Jan", "Nowak", "22222222222", "doctor1", "doctorpassword",
                                     Specialization.Cardiologist, "1234567"));
            employees.Add(new Doctor("Adam", "Małysz", "55555555555", "doctor3", "doctorpassword",
                         Specialization.Neurologist, "7654721"));
            employees.Add(new Doctor("Robert", "Kubica", "33333333333", "doctor2", "doctorpassword",
                                     Specialization.Neurologist, "7654321"));
            employees.Add(new Nurse("Aleksandra", "Nowak", "44444444444", "nurse1", "nursepass"));
        }
    }
}
