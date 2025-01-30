using System;
using HospitalSystem;

namespace HospitalSystem
{
    public class Administrator : Employee
    {
        public Administrator(string name, string surname, string pesel, string username, string password)
            : base(name, surname, pesel, username, password, Role.Administrator)
        {
        }
    }
}
